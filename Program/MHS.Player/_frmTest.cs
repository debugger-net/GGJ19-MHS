using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;


namespace MHS.Player
{
    public partial class _frmTest : Form
    {
        public _frmTest()
        {
            InitializeComponent();
        }

        private void _frmTest_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '+':
                    _GameSpeedIncrease();
                    break;

                case '-':
                    _GameSpeedDecrease();
                    break;
            }
        }

        private void _frmTest_Load(object sender, EventArgs e)
        {
            m_uiUpdateStateLock = new object();
            m_gameUIUpdateFuncDelegate = new GameUIUpdateFunc(_GameUIUpdateFunc);

            _InitializeGame();
            _InitializeCommandSystem();
            _InitializeGameUI();
            _InitializeSoundSystem();

            m_gameLoopThread = new Thread(_GameLoop);

            m_isGameRun = true;
            m_gameLoopThread.Start();
        }

        private void _frmTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_isGameRun = false;
            m_gameLoopThread.Join();
        }


        #region Game

        private Thread  m_gameLoopThread;
        private bool m_isGameRun;

        private Game.MHSGame m_game;

        private DateTime    m_lastUpdateTime;
        private TimeSpan[]  m_updateInterval;
        private int         m_currentGameSpeed;

        private void _InitializeGame()
        {
            m_game = Game.MHSGame.CreateNewGame();

            m_lastUpdateTime = DateTime.UtcNow;
            m_updateInterval = new TimeSpan[]
            {
                TimeSpan.FromSeconds(5.0),
                TimeSpan.FromSeconds(2.0),
                TimeSpan.FromSeconds(1.0),
                TimeSpan.FromSeconds(0.5),
                TimeSpan.FromSeconds(0.2),
                TimeSpan.FromSeconds(0.1),
                TimeSpan.FromSeconds(0.04),
            };
            m_currentGameSpeed = 2;
        }

        private void _GameLoop()
        {
            while (m_isGameRun)
            {
                DateTime currentFrameTime = DateTime.UtcNow;
                TimeSpan currentUpdateInterval = m_updateInterval[m_currentGameSpeed];

                if (currentFrameTime - m_lastUpdateTime >= currentUpdateInterval)
                {
                    Core.LogicCommand frameCommand = _GetFrameCommand();
                    Game.Processing.StepResult stepProcessResult = m_game.ProceedStep(frameCommand);

                    m_lastUpdateTime = currentFrameTime;
                }

                if (!m_isUIUpdating)
                {
                    this.Invoke(m_gameUIUpdateFuncDelegate);
                }

                _UpdateSoundSystem();

                Thread.Yield();
            }
        }

        private void _GameSpeedIncrease()
        {
            int nextSpeed = m_currentGameSpeed + 1;
            if (nextSpeed >= m_updateInterval.Length)
            {
                return; 
            }

            m_currentGameSpeed = nextSpeed;
            lbliGameSpeed.Text = string.Format("(Game Speed - {0})", nextSpeed);
        }

        private void _GameSpeedDecrease()
        {
            int nextSpeed = m_currentGameSpeed - 1;
            if (nextSpeed < 0)
            {
                return;
            }

            m_currentGameSpeed = nextSpeed;
            lbliGameSpeed.Text = string.Format("(Game Speed - {0})", nextSpeed);
        }


        #region From Interaction

        private void btnGameSpeedPlus_Click(object sender, EventArgs e)
        {
            _GameSpeedIncrease();
        }

        private void btnGameSpeedMinus_Click(object sender, EventArgs e)
        {
            _GameSpeedDecrease();
        }

        #endregion

        #endregion


        #region Game Command

        private Core.LogicCommand m_waitingCommand;
        private object m_commandLock;


        private Core.LogicCommand _GetFrameCommand()
        {
            lock (m_commandLock)
            {
                Core.LogicCommand existingCommand = m_waitingCommand;
                if (existingCommand != null)
                {
                    m_waitingCommand = null;
                    return existingCommand;
                }
            }

            return Core.LogicCommand.IdleCommand;
        }

        private void _InitializeCommandSystem()
        {
            m_waitingCommand = null;
            m_commandLock = new object();
        }

        private void _IssueCommand_StartBroadcasting()
        {
            lock (m_commandLock)
            {
                if (m_waitingCommand != null)
                {
                    return;
                }

                m_waitingCommand = new Game.Broadcasting.StartBroadcastingCommand(null);
            }
            _SetSoundStreamingOn();
        }

        private void _IssueCommand_FinishBroadcasting()
        {
            lock (m_commandLock)
            {
                if (m_waitingCommand != null)
                {
                    return;
                }

                m_waitingCommand = new Game.Broadcasting.FinishBroadcastingCommand();
            }
            _SetSoundStreamingOff();
        }

        private void _IssueCommand_ShoppingPurchase(long shopId, long shopItemId)
        {
            lock (m_commandLock)
            {
                if (m_waitingCommand != null)
                {
                    return;
                }

                m_waitingCommand = new Core.LogicCommands.ShoppingPurchaseCommand()
                {
                    purchasingShopId = shopId, purchasingItemId = shopItemId
                };
            }
        }


        #region Form Interaction

        private void btnStreamingStart_Click(object sender, EventArgs e)
        {
            if (m_game.MyChannel.CurrentStreaming != null)
            {
                return;
            }

            _IssueCommand_StartBroadcasting();
        }

        private void btnStreamingFinish_Click(object sender, EventArgs e)
        {
            if (m_game.MyChannel.CurrentStreaming == null)
            {
                return;
            }

            _IssueCommand_FinishBroadcasting();
        }

        #endregion

        #endregion


        #region Game UI

        private bool    m_isUIUpdating;
        private object  m_uiUpdateStateLock;

        private delegate void GameUIUpdateFunc();
        private GameUIUpdateFunc m_gameUIUpdateFuncDelegate;

        private Core.Common.GameLogicTime m_lastShownLogicTime;
        private long m_lastShownSubscribers;
        private long m_lastShownMoney;
        private int m_lastShownHealth;
        private int m_lastShownMental;
        private bool m_lastShownIsStreaming;


        private Color m_kColorStreaming = Color.FromArgb(128, 255, 128);
        private const string m_kTextMessageStreaming = "On Streaming";
        private Color m_kColorNoStreaming = Color.DimGray;
        private const string m_kTextMessageNoStreaming = "Not Streaming";


        private void _InitializeGameUI()
        {
            m_lastShownLogicTime = new Core.Common.GameLogicTime(-1);

            m_lastShownSubscribers = 0;
            m_lastShownMoney = 0;

            m_lastShownHealth = 0;
            m_lastShownMental = 0;

            lbliStreamingStatus.Text = m_kTextMessageNoStreaming;
            lbliStreamingStatus.BackColor = m_kColorNoStreaming;
            m_lastShownIsStreaming = false;

            _InitializeLogSystem();
        }

        private void _GameUIUpdateFunc()
        {
            lock (m_uiUpdateStateLock)
            {
                if (m_isUIUpdating)
                {
                    return;
                }

                m_isUIUpdating = true;
            }

            try
            {
                Core.Common.GameLogicTime currentGameTime = m_game.WorldState.LogicTime;
                if (currentGameTime != m_lastShownLogicTime)
                {
                    lbliCurrentTime.Text = string.Format("{0}일째 ({1})  {2:00}:{3:00}", 
                        currentGameTime.Days + 1, currentGameTime.DayOfWeek, currentGameTime.Hours, currentGameTime.Minutes);
                    m_lastShownLogicTime.Set(currentGameTime);
                }

                long currentSubscriberNumber = m_game.MyChannel.SubscriberNumber;
                if (currentSubscriberNumber != m_lastShownSubscribers)
                {
                    lbliSubscribers.Text = currentSubscriberNumber.ToString();
                    m_lastShownSubscribers = currentSubscriberNumber;
                }

                long currentMoney = m_game.Protagonist.CurrentMoney;
                if (currentMoney != m_lastShownMoney)
                {
                    lbliMoney.Text = currentMoney.ToString();
                    m_lastShownMoney = currentMoney;
                }

                int currentShowingHealth = (int)(m_game.Protagonist.HealthState * 100.0);
                if (currentShowingHealth != m_lastShownHealth)
                {
                    lbliHealth.Text = currentShowingHealth.ToString();
                    m_lastShownHealth = currentShowingHealth;
                }

                int currentShowingMental = (int)(m_game.Protagonist.MentalState * 100.0);
                if (currentShowingMental != m_lastShownMental)
                {
                    lbliMental.Text = currentShowingMental.ToString();
                    m_lastShownMental = currentShowingMental;
                }

                bool isStreaming = (m_game.MyChannel.CurrentStreaming != null);
                if (isStreaming != m_lastShownIsStreaming)
                {
                    if (isStreaming)
                    {
                        lbliStreamingStatus.Text = m_kTextMessageStreaming;
                        lbliStreamingStatus.BackColor = m_kColorStreaming;
                    }
                    else
                    {
                        lbliStreamingStatus.Text = m_kTextMessageNoStreaming;
                        lbliStreamingStatus.BackColor = m_kColorNoStreaming;
                    }
                    m_lastShownIsStreaming = isStreaming;
                }
            }
            finally
            {
                lock (m_uiUpdateStateLock)
                {
                    m_isUIUpdating = false;
                }
            }
        }


        #region Logs

        private ConcurrentQueue<string> m_logQueue;

        private bool m_isLogUpdating;
        private object m_logUpdateStateLock;

        private GameUIUpdateFunc m_gameLogUpdateFuncDelegate;

        private void _InitializeLogSystem()
        {
            m_logQueue = new ConcurrentQueue<string>();

            m_isLogUpdating = false;
            m_logUpdateStateLock = new object();

            m_gameLogUpdateFuncDelegate = new GameUIUpdateFunc(_LogUpdateFunc);
        }

        private void _LogUpdateFunc()
        {
            lock (m_logUpdateStateLock)
            {
                if (m_isLogUpdating)
                {
                    return;
                }

                m_isLogUpdating = true;
            }

            string currentLog = null;
            while (m_logQueue.TryDequeue(out currentLog))
            {
                txtLog.AppendText(Environment.NewLine);
                txtLog.AppendText(currentLog);
            }

            lock (m_logUpdateStateLock)
            {
                m_isLogUpdating = false;
            }
        }

        /// <summary>
        /// Write Game Log
        /// </summary>
        /// <param name="logString">Log to write</param>
        public void WriteLog(string logString)
        {
            m_logQueue.Enqueue(logString);
            this.Invoke(m_gameLogUpdateFuncDelegate);
        }

        #endregion

        #endregion


        #region Sound

        private const string kSoundFilePath_Signal = "Sound/mhs_signal.wav";
        private const string kSoundFilePath_BGM = "Sound/mhs_bgm.wav";

        private WaveOutEvent m_soundDevice;

        private AudioFileReader m_soundFile_signal;
        private AudioFileReader m_soundFile_bgm;

        private bool m_soundIsStreaming;


        private void _InitializeSoundSystem()
        {
            m_soundDevice = new WaveOutEvent();

            m_soundFile_signal = new AudioFileReader(kSoundFilePath_Signal);
            m_soundFile_bgm = new AudioFileReader(kSoundFilePath_BGM);

            m_soundIsStreaming = false;
        }

        private void _UpdateSoundSystem()
        {
            if (m_soundIsStreaming)
            {
                if (m_soundDevice.PlaybackState == PlaybackState.Stopped)
                {
                    m_soundFile_bgm.Position = 0;
                    m_soundDevice.Init(m_soundFile_bgm);
                    m_soundDevice.Play();
                }
            }
        }

        private void _SetSoundStreamingOn()
        {
            m_soundIsStreaming = true;

            if (m_soundDevice.PlaybackState != PlaybackState.Stopped)
            {
                m_soundDevice.Stop();
            }
            m_soundFile_signal.Position = 0;
            m_soundDevice.Init(m_soundFile_signal);
            m_soundDevice.Play();
        }

        private void _SetSoundStreamingOff()
        {
            m_soundDevice.Stop();

            m_soundIsStreaming = false;
        }

        #endregion

        private void btnShopping_Click(object sender, EventArgs e)
        {
            _IssueCommand_ShoppingPurchase(
                m_game.Shopping.Shops.First().SerialNumber,
                m_game.Shopping.Shops.First().Catalogue.First().SerialNumber
                );
        }
    }
}

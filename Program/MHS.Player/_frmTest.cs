﻿using System;
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

                    _ProcessFrameResultLogic(frameCommand, stepProcessResult);

                    m_lastUpdateTime = currentFrameTime;
                }

                if (!m_isUIUpdating && m_isGameRun)
                {
                    this.BeginInvoke(m_gameUIUpdateFuncDelegate);
                }

                _UpdateSoundSystem();

                Application.DoEvents();
                Thread.Yield();
            }
        }

        private void _ProcessFrameResultLogic(Core.LogicCommand frameCommand, Game.Processing.StepResult stepProcessResult)
        {
            if (frameCommand.CoarseType == Core.LogicCommand.CommandCoarseType.kShopping)
            {
                Core.LogicCommands.ShoppingCommand shoppingCommand = frameCommand as Core.LogicCommands.ShoppingCommand;
                if (shoppingCommand.CommandType == Core.LogicCommands.ShoppingCommandType.kBuy)
                {
                    if (stepProcessResult.commandResult.isSuccess)
                    {
                        Core.Game.Shopping.Order order = (stepProcessResult.commandResult.payloadData as Game.Shop.PurchaseCommandResult).placedOrder;
                        Core.Game.Shopping.IShopItem purchasedItem = order.orderedShopItem;

                        WriteLog(string.Format("(Order No. {0}) Purchaed \"{1}\" at {2} money.", order.serialNumber, purchasedItem.View.SellingItemName, purchasedItem.Price));
                        _SoundPlayPurchase();
                    }
                }
            }
            else if (frameCommand.CoarseType == Core.LogicCommand.CommandCoarseType.kBroadcasting)
            {
                if (stepProcessResult.commandResult.isSuccess)
                {
                    if (frameCommand is Game.Broadcasting.StartBroadcastingCommand)
                    {
                        _SetSoundStreamingOn();
                    }
                    else if (frameCommand is Game.Broadcasting.FinishBroadcastingCommand)
                    {
                        _SetSoundStreamingOff();
                    }
                }
            }

            // Process Receive
            if (stepProcessResult.receivedItems.Count > 0)
            {
                foreach (Core.Game.Item currentReceivedItem in stepProcessResult.receivedItems)
                {
                    m_inventoryUIWaitingQueue.Enqueue(new InventoryListItemEntry(currentReceivedItem));
                    WriteLog(string.Format("Item Received!! - {0}: {1}", currentReceivedItem.View.VisibleName, currentReceivedItem.View.Description));

                    ++m_itemCount;
                }

                _SoundPlayReceived();
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

        private ConcurrentQueue<InventoryListItemEntry> m_inventoryUIWaitingQueue;


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

            m_inventoryUIWaitingQueue = new ConcurrentQueue<InventoryListItemEntry>();

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

                InventoryListItemEntry queuedItem;
                while (m_inventoryUIWaitingQueue.TryDequeue(out queuedItem))
                {
                    lstMyItems.Items.Add(queuedItem);
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
        private const string kSoundFilePath_Purchase = "Sound/mhs_sfx_purchase_01.wav";
        private const string kSoundFilePath_Received = "Sound/mhs_sfx_doorbell_02.wav";

        private static readonly string[] kSoundFilePath_BGMs = new string[]
        {
            "Sound/mhs_bgm_level1.wav",
            "Sound/mhs_bgm_level2.wav",
            "Sound/mhs_bgm_level3.wav"
        };

        private bool m_soundIsStreaming;
        private bool m_isPlaySignal;
        
        private Audio.AudioPlaybackEngine m_audioPlayer;
        private object m_soundSystemLock;

        private Audio.CachedSound m_cachedSound_signal;
        private List<Audio.CachedSound> m_cachedSound_bgms;

        private Audio.CachedSound m_cachedSound_purchase;
        private Audio.CachedSound m_cachedSound_received;

        private Audio.CachedSound m_streamBGMSound;
        private Audio.AudioPlaybackEngine.SoundSourceHandle m_streamSoundHandle;


        private void _InitializeSoundSystem()
        {
            m_soundIsStreaming = false;
            m_isPlaySignal = false;

            m_audioPlayer = Audio.AudioPlaybackEngine.Instance;
            m_soundSystemLock = new object();

            m_cachedSound_signal = new Audio.CachedSound(kSoundFilePath_Signal);

            m_cachedSound_bgms = new List<Audio.CachedSound>();
            for (int i = 0; i < kSoundFilePath_BGMs.Length; ++i)
            {
                m_cachedSound_bgms.Add(new Audio.CachedSound(kSoundFilePath_BGMs[i], true));
            }

            m_cachedSound_purchase = new Audio.CachedSound(kSoundFilePath_Purchase);
            m_cachedSound_received = new Audio.CachedSound(kSoundFilePath_Received);

            m_streamSoundHandle.insertedProvider = null;
            m_streamSoundHandle.sourceProvider = null;
        }

        private void _UpdateSoundSystem()
        {
            lock (m_soundSystemLock)
            {
                if (m_soundIsStreaming)
                {
                    if (m_isPlaySignal)
                    {
                        if (m_streamSoundHandle.sourceProvider.IsFinished)
                        {
                            m_audioPlayer.RemoveSoundFromMixer(m_streamSoundHandle);
                            m_streamSoundHandle = m_audioPlayer.PlayHandledSound(m_streamBGMSound);
                            m_isPlaySignal = false;
                        }
                    }
                    else
                    {
                        if (m_streamSoundHandle.sourceProvider.IsFinished)
                        {
                            m_audioPlayer.RemoveSoundFromMixer(m_streamSoundHandle);
                            m_streamSoundHandle = m_audioPlayer.PlayHandledSound(m_streamBGMSound);
                        }
                    }
                }
            }
        }

        private void _SetSoundStreamingOn()
        {
            int level = _GetCurrentStreamingQualityLevel();
            if (level >= m_cachedSound_bgms.Count)
            {
                level = m_cachedSound_bgms.Count - 1;
            }

            lock (m_soundSystemLock)
            {
                m_soundIsStreaming = true;
                m_isPlaySignal = true;

                m_streamBGMSound = m_cachedSound_bgms[level];

                m_streamSoundHandle = m_audioPlayer.PlayHandledSound(m_cachedSound_signal);
            }
        }

        private void _SetSoundStreamingOff()
        {
            lock (m_soundSystemLock)
            {
                m_soundIsStreaming = false;

                if (m_streamSoundHandle.insertedProvider != null)
                {
                    m_audioPlayer.RemoveSoundFromMixer(m_streamSoundHandle);
                    m_streamSoundHandle.insertedProvider = null;
                    m_streamSoundHandle.sourceProvider = null;
                }
            }
        }

        private void _SoundPlayPurchase()
        {
            m_audioPlayer.PlaySound(m_cachedSound_purchase);
        }

        private void _SoundPlayReceived()
        {
            m_audioPlayer.PlaySound(m_cachedSound_received);
        }

        #endregion

        private void btnShopping_Click(object sender, EventArgs e)
        {
            _IssueCommand_ShoppingPurchase(
                m_game.Shopping.Shops.First().SerialNumber,
                m_game.Shopping.Shops.First().Catalogue.First().SerialNumber
                );
        }

        private int m_itemCount = 0;
        private int _GetCurrentStreamingQualityLevel()
        {
            // Temp Impl.
            return m_itemCount / 2;
        }


        internal class InventoryListItemEntry
        {
            public InventoryListItemEntry(Core.Game.Item itemEntry)
            {
                m_item = itemEntry;
            }

            private Core.Game.Item m_item;

            public override string ToString()
            {
                return string.Format("({0}) {1}", m_item.serialNumber, m_item.View.SummaryString);
            }
        }
    }
}

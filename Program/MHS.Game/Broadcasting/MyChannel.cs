using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Game
{
    /// <summary>
    /// Protagonist's broadcasting channel
    /// </summary>
    internal class MyChannel : Core.Interface.IChannelState
    {
        public MyChannel(Protagonist streamer)
        {
            m_streamer = streamer;

            m_currentSubscriberNumber = 0;
            MaximumSubscriberNumber = 0;

            m_currentStreaming = null;
            m_finishedVideoRecords = new List<Core.Game.IChannelVideo>();
        }


        public void UpdateStep()
        {

        }

        public void ApplyPeopleResult(PeopleSystem.UpdateEffect updateEffect)
        {
            SubscriberNumber += updateEffect.SubscriberChange;

            if (m_currentStreaming != null)
            {
                m_currentStreaming.NotifyStepEffect(updateEffect);
            }
        }


        public void StartStreamingSession(Broadcasting.StreamingSession session)
        {
            m_currentStreaming = session;

            if (m_currentStreaming.streamingContents != null)
            {
                m_currentStreaming.streamingContents.OnStreamingStarted();
            }
        }

        public bool FinishStreamingSession()
        {
            if (m_currentStreaming == null)
            {
                return false;
            }

            if (m_currentStreaming.streamingContents != null)
            {
                m_currentStreaming.streamingContents.OnStreamingFinished();
            }

            m_finishedVideoRecords.Add(m_currentStreaming.ToVideo());
            m_currentStreaming = null;

            return true;
        }


        #region State Interface

        /// <summary>Streamer of the channel (= Protagonist)</summary>
        public Core.Interface.IProtagonistState Streamer
        {
            get { return m_streamer; }
        }
        private Protagonist m_streamer;


        /// <summary>Number of current subscribers.</summary>
        public long SubscriberNumber
        {
            get { return m_currentSubscriberNumber; }
            private set
            {
                if (value < 0)
                {
                    m_currentSubscriberNumber = 0;
                }
                else
                {
                    m_currentSubscriberNumber = value;
                }

                if (value > MaximumSubscriberNumber)
                {
                    MaximumSubscriberNumber = value;
                }
            }
        }
        private long m_currentSubscriberNumber;

        /// <summary>Number of maximum subscriber number in channel history.</summary>
        public long MaximumSubscriberNumber { get; private set; }


        /// <summary>Current streaming contents. null if currently not streaming.</summary>
        public Core.Game.ICurrentStreaming CurrentStreaming { get { return m_currentStreaming; } }
        Broadcasting.StreamingSession m_currentStreaming;

        /// <summary>List of finished streaming videos.</summary>
        public List<Core.Game.IChannelVideo> FinishedVideos { get { return m_finishedVideoRecords; } }
        List<Core.Game.IChannelVideo> m_finishedVideoRecords;

        #endregion
    }
}

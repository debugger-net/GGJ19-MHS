using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Game.Broadcasting
{
    /// <summary>
    /// Currently playing streaming session
    /// </summary>
    internal class StreamingSession : Core.Game.ICurrentStreaming
    {
        public StreamingSession(StreamingContents contents)
        {
            SerialNumber = s_IssueSerialNumber();

            streamingContents = contents;

            m_retainedSubscriber = 0;
            m_leavedSubscriber = 0;
        }

        /// <summary>
        /// Unique Serial Number of the Stream
        /// </summary>
        public long SerialNumber { get; private set; }

        /// <summary>
        /// Contents of the streaming
        /// </summary>
        public Core.Game.Broadcasting.IStreamingContents Contents { get { return streamingContents; } }

        /// <summary>
        /// Contents of the streaming
        /// </summary>
        internal readonly StreamingContents streamingContents;


        public void NotifyStepEffect(PeopleSystem.UpdateEffect effect)
        {
            m_retainedSubscriber += effect.retainedSubscriber;
            m_leavedSubscriber += effect.leavedSubscriber;
        }


        public ChannelVideo ToVideo()
        {
            ChannelVideo recordedVideo = new ChannelVideo(SerialNumber, streamingContents, m_retainedSubscriber, m_leavedSubscriber);
            

            return recordedVideo;
        }


        private int m_retainedSubscriber;
        private int m_leavedSubscriber;


        #region Serial Number

        static StreamingSession()
        {
            ms_currentSerialNumber = 0;
        }

        private static long ms_currentSerialNumber;

        private static long s_IssueSerialNumber()
        {
            ++ms_currentSerialNumber;
            long serialNumber = ms_currentSerialNumber;

            return serialNumber;
        }

        #endregion
    }
}

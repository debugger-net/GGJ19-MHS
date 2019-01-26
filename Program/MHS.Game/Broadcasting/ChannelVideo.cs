using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Game.Broadcasting
{
    /// <summary>
    /// Video in a channel which is finished streaming
    /// </summary>
    internal class ChannelVideo : Core.Game.IChannelVideo
    {
        public ChannelVideo(long serialNumber, StreamingContents contents, int retainedSubscriber, int leavedSubscriber)
        {
            SerialNumber = serialNumber;

            streamingContents = contents;

            m_retainedSubscriberDuringVideo = retainedSubscriber;
            m_leavedSubscriberDuringVideo = leavedSubscriber;
        }


        /// <summary>
        /// Unique serial number of the played stream
        /// </summary>
        public long SerialNumber { get; private set; }


        /// <summary>Contents of the video</summary>
        public Core.Game.Broadcasting.IStreamingContents Contents { get { return streamingContents; } }

        /// <summary>
        /// Contents of the original streaming
        /// </summary>
        internal readonly StreamingContents streamingContents;


        /// <summary>Number of subscribers came to channel during the video streaming</summary>
        public int RetainedSubscriberDuringVideo { get { return m_retainedSubscriberDuringVideo; } }
        private readonly int m_retainedSubscriberDuringVideo;

        /// <summary>Number of subscribers leaved channel during the video streaming</summary>
        public int LeavedSubscriberDuringVideo { get { return m_leavedSubscriberDuringVideo; } }
        private readonly int m_leavedSubscriberDuringVideo;
    }
}

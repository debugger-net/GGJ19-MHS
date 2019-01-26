using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Core.Game
{
    /// <summary>
    /// Interface for finished (and uploaded) video contents
    /// </summary>
    public interface IChannelVideo
    {
        /// <summary>
        /// Unique serial number of the played stream
        /// </summary>
        long SerialNumber { get; }


        /// <summary>Contents of the video</summary>
        Broadcasting.IStreamingContents Contents { get; }


        /// <summary>Number of subscribers came to channel during the video streaming</summary>
        int RetainedSubscriberDuringVideo { get; }

        /// <summary>Number of subscribers leaved channel during the video streaming</summary>
        int LeavedSubscriberDuringVideo { get; }
    }
}

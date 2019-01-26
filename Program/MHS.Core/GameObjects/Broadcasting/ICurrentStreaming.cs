using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Core.Game
{
    /// <summary>
    /// Interface for current streaming video in the channel
    /// </summary>
    public interface ICurrentStreaming
    {
        /// <summary>
        /// Unique Serial Number of the Stream
        /// </summary>
        long SerialNumber { get; }


        /// <summary>
        /// Contents of the streaming
        /// </summary>
        Broadcasting.IStreamingContents Contents { get; }
    }
}

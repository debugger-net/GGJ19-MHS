using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Core.Interface
{
    /// <summary>
    /// Interface for state of a Channel (only MyChannel)
    /// </summary>
    public interface IChannelState
    {
        /// <summary>Streamer of the channel (= Protagonist)</summary>
        IProtagonistState Streamer { get; }


        /// <summary>Number of current subscribers.</summary>
        long SubscriberNumber { get; }

        /// <summary>Number of maximum subscriber number in channel history.</summary>
        long MaximumSubscriberNumber { get; }


        /// <summary>Current streaming contents. null if currently not streaming.</summary>
        Game.ICurrentStreaming CurrentStreaming { get; }

        /// <summary>List of finished streaming videos.</summary>
        List<Game.IChannelVideo> FinishedVideos { get; }


        //NOTE: Will add channel quaility by electronics and so on...
    }
}

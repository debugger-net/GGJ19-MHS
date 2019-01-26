using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Game.Broadcasting
{
    /// <summary>
    /// Base class for streaming contents
    /// </summary>
    public abstract class StreamingContents : Core.Game.Broadcasting.IStreamingContents
    {
        /// <summary>
        /// Called when the streaming started
        /// </summary>
        public virtual void OnStreamingStarted() { }

        /// <summary>
        /// Called when the streaming finished
        /// </summary>
        public virtual void OnStreamingFinished() { }
    }
}

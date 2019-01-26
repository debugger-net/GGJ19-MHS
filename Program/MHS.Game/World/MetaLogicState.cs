using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Game
{
    /// <summary>
    /// Meta logic state of a game session
    /// </summary>
    internal class MetaLogicState
    {
        /// <summary>Next channel paying time</summary>
        public Core.Common.GameLogicTime NextPayTime { get; set; }


        /// <summary>
        /// Initialize default state
        /// </summary>
        public MetaLogicState()
        {
            NextPayTime = Contents.Meta.MetaConstants.kChannelPayStart;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Core.LogicCommands
{
    /// <summary>
    /// Command to do NOTHING
    /// </summary>
    public class IdleCommand : LogicCommand
    {
        /// <summary>
        /// Coarse-level type of the command
        /// </summary>
        public override CommandCoarseType CoarseType { get { return CommandCoarseType.kIdle; } }
    }
}

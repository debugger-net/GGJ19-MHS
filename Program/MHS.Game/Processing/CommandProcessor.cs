using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Game.Processing
{
    /// <summary>
    /// Game logic command processor
    /// </summary>
    internal class CommandProcessor
    {
        /// <summary>
        /// Apply command to the world
        /// </summary>
        /// <param name="targetWorld">Target world</param>
        /// <param name="stepCommand">Applying command</param>
        public void ApplyCommand(World targetWorld, Core.LogicCommand stepCommand)
        {
            switch (stepCommand.CoarseType)
            {
                case Core.LogicCommand.CommandCoarseType.kIdle:
                    return;
            }
        }
    }
}

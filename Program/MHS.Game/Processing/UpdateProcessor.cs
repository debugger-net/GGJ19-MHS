using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Game.Processing
{
    /// <summary>
    /// Game state update processor
    /// </summary>
    internal class UpdateProcessor
    {
        /// <summary>
        /// Update world state one step
        /// </summary>
        /// <param name="targetWorld">Updating world</param>
        public void UpdateWorldOneStep(World targetWorld)
        {
            targetWorld.LogicTime.AddTime(Core.Common.GameLogicTime.Unit);


        }
    }
}

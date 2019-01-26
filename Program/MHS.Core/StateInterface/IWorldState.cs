using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Core.Interface
{
    /// <summary>
    /// World-level state interface
    /// </summary>
    public interface IWorldState
    {
        /// <summary>
        /// Current world game time
        /// </summary>
        Common.GameLogicTime LogicTime { get; }
    }
}

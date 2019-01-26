using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Core
{
    /// <summary>
    /// Logical command to the game
    /// </summary>
    public abstract class LogicCommand
    {
        /// <summary>
        /// Coarse-level command type
        /// </summary>
        public enum CommandCoarseType
        {
            kIdle = 0, 
            kShopping,
            kEnvironmental, 
            kBroadcasting,
            kSubSystemDependents, 
        }

        /// <summary>
        /// Coarse-level type of the command
        /// </summary>
        public abstract CommandCoarseType CoarseType { get; }


        /// <summary>
        /// Idle Command
        /// </summary>
        public static LogicCommands.IdleCommand IdleCommand { get; private set; }


        #region Class Common

        static LogicCommand()
        {
            IdleCommand = new LogicCommands.IdleCommand();
        }

        #endregion
    }
}

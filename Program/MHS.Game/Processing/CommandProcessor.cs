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
        /// <returns>Result of command processing</returns>
        public Core.CommandResult ApplyCommand(World targetWorld, Core.LogicCommand stepCommand)
        {
            switch (stepCommand.CoarseType)
            {
                case Core.LogicCommand.CommandCoarseType.kIdle:
                    return Core.CommandResult.SimpleSuccess;

                case Core.LogicCommand.CommandCoarseType.kBroadcasting:
                    Broadcasting.BroadcastingCommand broadcastingCommand = stepCommand as Broadcasting.BroadcastingCommand;
                    return broadcastingCommand.DoCommand(targetWorld);
            }

            return new Core.CommandResult(false, new CommandFailReason_CannotProcess());
        }


        #region Internal

        internal CommandProcessor(IGameContext gameContext)
        {
            m_gameContext = gameContext;
        }

        private IGameContext m_gameContext;

        #endregion
    }


    /// <summary>
    /// Command processing fail reason for not processable command
    /// </summary>
    public class CommandFailReason_CannotProcess : Core.ICommandFailReason
    {
        /// <summary>
        /// Readable reason message
        /// </summary>
        /// <returns>Message string</returns>
        public override string Why() { return "No processing logic for the command."; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Game.Processing
{
    /// <summary>
    /// Game logic step processor
    /// </summary>
    internal class StepProcessor
    {
        /// <summary>
        /// Process a game step proceeding logic
        /// </summary>
        /// <param name="targetWorld">World to apply step logic process</param>
        /// <param name="stepCommand">A command given to current step</param>
        /// <returns>Step processing result</returns>
        public StepResult ProcessStep(World targetWorld, Core.LogicCommand stepCommand)
        {
            m_commandProcessor.ApplyCommand(targetWorld, stepCommand);
            m_updateProcessor.UpdateWorldOneStep(targetWorld);

            // Gather Results

            return new StepResult();
        }


        /// <summary>
        /// Construct a new processor
        /// </summary>
        public StepProcessor(IGameContext gameContext)
        {
            m_gameContext = gameContext;

            m_commandProcessor = new CommandProcessor(gameContext);
            m_updateProcessor = new UpdateProcessor(gameContext);
        }

        private IGameContext m_gameContext;

        private CommandProcessor m_commandProcessor;
        private UpdateProcessor m_updateProcessor;
    }
}

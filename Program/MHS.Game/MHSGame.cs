using System;

namespace MHS.Game
{
    /// <summary>
    /// My Home Studio Logical Game Instance Class
    /// </summary>
    public class MHSGame
    {
        #region Creations

        /// <summary>
        /// Create a New Game
        /// </summary>
        /// <returns>Crreated Game Object</returns>
        public static MHSGame CreateNewGame()
        {
            MHSGame gameObject = new MHSGame();
            gameObject.InitializeNewGame();

            return gameObject;
        }

        #endregion


        #region Operations

        /// <summary>
        /// Proceed a game step
        /// </summary>
        /// <param name="stepCommand">A command given to current step</param>
        /// <returns>Step processing result</returns>
        public Processing.StepResult ProceedStep(Core.LogicCommand stepCommand)
        {
            return m_stepProcessor.ProcessStep(m_world, stepCommand);
        }

        #endregion


        #region Game State Interfaces

        /// <summary>
        /// Protagonist state
        /// </summary>
        public Core.Interface.IProtagonistState Protagonist { get { return m_world.Protagonist; } }

        #endregion


        #region Game Initialization

        /// <summary>
        /// Default constructor to initialize basic contents of the object
        /// </summary>
        internal MHSGame()
        {
            m_stepProcessor = new Processing.StepProcessor();
        }

        /// <summary>
        /// Initialize a New Game
        /// </summary>
        internal void InitializeNewGame()
        {
            m_world = World.CreateAWholeNewWorld();
        }

        #endregion


        #region Game Internal

        private World m_world;

        private Processing.StepProcessor m_stepProcessor;

        #endregion
    }
}

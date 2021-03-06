﻿using System;

namespace MHS.Game
{
    /// <summary>
    /// My Home Studio Logical Game Instance Class
    /// </summary>
    public class MHSGame : IGameContext
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
        /// World state
        /// </summary>
        public Core.Interface.IWorldState WorldState { get { return m_world; } }

        /// <summary>
        /// Protagonist state
        /// </summary>
        public Core.Interface.IProtagonistState Protagonist { get { return m_world.Protagonist; } }

        /// <summary>
        /// Broadcasting channel state
        /// </summary>
        public Core.Interface.IChannelState MyChannel { get { return m_world.MyChannel; } }

        /// <summary>
        /// Shopping system state
        /// </summary>
        public Core.Interface.IShoppingState Shopping { get { return m_world.Shoppings; } }

        #endregion


        #region Game Initialization

        /// <summary>
        /// Default constructor to initialize basic contents of the object
        /// </summary>
        private MHSGame()
        {
            m_gameData = GameData.LoadGameData();

            m_stepProcessor = new Processing.StepProcessor(this);
        }

        /// <summary>
        /// Initialize a New Game
        /// </summary>
        private void InitializeNewGame()
        {
            m_world = World.CreateAWholeNewWorld(this);
        }

        #endregion


        #region Game Internal

        private GameData m_gameData;

        private World m_world;

        private Processing.StepProcessor m_stepProcessor;

        #endregion


        #region Context Interface

        /// <summary>Game Contents Data</summary>
        public GameData Data { get { return m_gameData; } }

        #endregion
    }
}

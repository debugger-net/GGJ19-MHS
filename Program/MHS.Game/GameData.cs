using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Game
{
    /// <summary>
    /// Game contents data
    /// </summary>
    public class GameData
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GameData()
        {
            InCodeData = new Contents.CodedData();
        }

        /// <summary>
        /// Load all game logic data
        /// </summary>
        public static GameData LoadGameData()
        {
            GameData createdDataObject = new GameData();

            return createdDataObject;
        }

        /// <summary>
        /// In-Code level data
        /// </summary>
        public Contents.CodedData InCodeData { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Game
{
    /// <summary>
    /// Game context interface
    /// </summary>
    internal interface IGameContext
    {
        /// <summary>Game Contents Data</summary>
        GameData Data { get; }
    }
}

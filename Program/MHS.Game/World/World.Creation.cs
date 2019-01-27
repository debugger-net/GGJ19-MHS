using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Game
{
    // Defines creation and initialization related codes for the world

    /// <summary>
    /// Contains all stateful objects in current session timeline world
    /// </summary>
    internal partial class World : Core.Interface.IWorldState
    {
        /// <summary>
        /// Make a new gameplay world
        /// </summary>
        /// <returns>Created new world</returns>
        public static World CreateAWholeNewWorld(IGameContext gameContext)
        {
            World creatingWorld = new World();

            creatingWorld.LogicTime = Core.Common.GameLogicTime.StandardStarting;
            creatingWorld.MetaLogicState = new MetaLogicState();

            creatingWorld.Protagonist = new Protagonist();
            creatingWorld.MyHome = new MyHome();

            creatingWorld.Environments = new Environments();

            creatingWorld.MyChannel = new MyChannel(creatingWorld.Protagonist);
            creatingWorld.PeopleSystem = new PeopleSystem(gameContext);

            creatingWorld.Shoppings = new Shoppings(gameContext);
            creatingWorld.DeliverySystem = new DeliverySystem();

            return creatingWorld;
        }
    }
}

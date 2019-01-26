using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Game
{
    // Defines creation and initialization related codes for the world

    /// <summary>
    /// Contains all stateful objects in current session timeline world
    /// </summary>
    public partial class World
    {
        /// <summary>
        /// Make a new gameplay world
        /// </summary>
        /// <returns>Created new world</returns>
        public static World CreateAWholeNewWorld()
        {
            World creatingWorld = new World();

            creatingWorld.LogicTime = Core.Common.GameLogicTime.StandardStarting;

            creatingWorld.Protagonist = new Protagonist();
            creatingWorld.MyHome = new MyHome();

            creatingWorld.Environments = new Environments();

            creatingWorld.MyChannel = new MyChannel();
            creatingWorld.PeopleSystem = new PeopleSystem();

            creatingWorld.Shoppings = new Shoppings();
            creatingWorld.DeliverySystem = new DeliverySystem();

            return creatingWorld;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Game
{
    /// <summary>
    /// Contains all stateful objects in current session timeline world
    /// </summary>
    internal partial class World : Core.Interface.IWorldState
    {
        /// <summary>
        /// Current world game time
        /// </summary>
        public Core.Common.GameLogicTime LogicTime { get; private set; }

        /// <summary>
        /// World meta-logic state
        /// </summary>
        public MetaLogicState MetaLogicState { get; private set; }


        /// <summary>
        /// Protagonist of the world
        /// </summary>
        public Protagonist Protagonist { get; private set; }

        /// <summary>
        /// THE HOME in the world
        /// </summary>
        public MyHome MyHome { get; private set; }


        /// <summary>
        /// World's environmental states
        /// </summary>
        public Environments Environments { get; private set; }


        /// <summary>
        /// Protagonist's broadcasting channel
        /// </summary>
        public MyChannel MyChannel { get; private set; }
        
        /// <summary>
        /// Latent viewer public of the world
        /// </summary>
        public PeopleSystem PeopleSystem { get; private set; }


        /// <summary>
        /// Shopping-related states of the world
        /// </summary>
        public Shoppings Shoppings { get; private set; }

        /// <summary>
        /// Delivery-related states of the world
        /// </summary>
        public DeliverySystem DeliverySystem { get; private set; }

    }
}

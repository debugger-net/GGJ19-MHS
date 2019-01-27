using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Core.Game.Shopping
{
    /// <summary>
    /// Shop selling item interface
    /// </summary>
    public interface IShopItem : IItemGenerator
    {
        /// <summary>
        /// Unique serial number
        /// </summary>
        long SerialNumber { get; }

        /// <summary>
        /// Price of the Item
        /// </summary>
        long Price { get; }

        /// <summary>
        /// View of the selling item
        /// </summary>
        IShopItemView View { get; }
    }


    /// <summary>
    /// Item generator logic object interface
    /// </summary>
    public interface IItemGenerator
    {
        /// <summary>
        /// Generate real item instance
        /// </summary>
        /// <param name="worldState">Current world state</param>
        /// <returns>Generated item</returns>
        Item Generate(Interface.IWorldState worldState);
    }
}

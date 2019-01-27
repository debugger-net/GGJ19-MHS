using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Core.Game.Shopping
{
    /// <summary>
    /// Interface for in-game shop object
    /// </summary>
    public interface IShop
    {
        /// <summary>
        /// Unique serial number
        /// </summary>
        long SerialNumber { get; }

        /// <summary>
        /// Get current selling catalogue
        /// </summary>
        IEnumerable<IShopItem> Catalogue { get; }
    }


    /// <summary>
    /// Order processor logic object interface
    /// </summary>
    public interface IShopOrderProcessor
    {
        /// <summary>
        /// Update state of order and return is ready to deliver.
        /// </summary>
        /// <param name="order">Updating order</param>
        /// <param name="worldState">Current world state</param>
        /// <returns>Whether is ready to deliver</returns>
        bool UpdateOrder(Order order, Core.Interface.IWorldState worldState);
    }
}

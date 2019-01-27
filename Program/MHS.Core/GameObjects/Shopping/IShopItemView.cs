using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Core.Game.Shopping
{
    /// <summary>
    /// View data of a shop item for game client
    /// </summary>
    public interface IShopItemView
    {
        /// <summary>
        /// Showing Name of Shop Item
        /// </summary>
        string SellingItemName { get; }
    }
}

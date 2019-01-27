using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Contents.Shopping
{
    /// <summary>
    /// Base class of shop data
    /// </summary>
    public abstract class ShopData
    {
        /// <summary>
        /// Shop item cataluge data of the shop
        /// </summary>
        public abstract IEnumerable<ShopItemData> CatalogueData { get; }


        /// <summary>
        /// Generate shop's order processor
        /// </summary>
        /// <returns>Created order processor</returns>
        public abstract Core.Game.Shopping.IShopOrderProcessor GenerateOrderProcessor();
    }
}

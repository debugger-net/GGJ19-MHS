using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Contents.Shopping
{
    /// <summary>
    /// Base class of each shop item data
    /// </summary>
    public abstract class ShopItemData
    {
        /// <summary>
        /// Get a real item generator of the shop item
        /// </summary>
        public abstract Core.Game.Shopping.IItemGenerator ItemGenerator { get; }

        /// <summary>
        /// Get a view for the shop item
        /// </summary>
        public abstract Core.Game.Shopping.IShopItemView View { get; }

        /// <summary>
        /// Price to purchase
        /// </summary>
        public abstract long Price { get; }
    }


    /// <summary>
    /// Sub-object consisting basic shop item
    /// </summary>
    public class BasicShopItemData : ShopItemData
    {
        /// <summary>
        /// Construct shop item data by consisting data objects
        /// </summary>
        /// <param name="price">Item proce</param>
        /// <param name="itemGenerator">Real item generator</param>
        /// <param name="view">Shop item view</param>
        public BasicShopItemData(long price, Core.Game.Shopping.IItemGenerator itemGenerator, Core.Game.Shopping.IShopItemView view)
        {
            m_price = price;

            m_itemGenerator = itemGenerator;
            m_view = view;
        }

        /// <summary>
        /// Get a real item generator of the shop item
        /// </summary>
        public override Core.Game.Shopping.IItemGenerator ItemGenerator { get { return m_itemGenerator; }  }
        protected Core.Game.Shopping.IItemGenerator m_itemGenerator;

        /// <summary>
        /// Get a view for the shop item
        /// </summary>
        public override Core.Game.Shopping.IShopItemView View { get { return m_view; } }
        protected Core.Game.Shopping.IShopItemView m_view;

        /// <summary>
        /// Price to purchase
        /// </summary>
        public override long Price { get { return m_price; } }
        protected long m_price;
    }
}

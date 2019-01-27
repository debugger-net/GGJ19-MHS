using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MHS.Game.Shop
{
    /// <summary>
    /// Selling item entry in a shop
    /// </summary>
    internal class ShopItem : Core.Game.Shopping.IShopItem
    {
        public ShopItem(long price, Core.Game.Shopping.IShopItemView view, Core.Game.Shopping.IItemGenerator itemGenerator)
        {
            SerialNumber = s_IssueSerialNumber();
            m_price = price;
            View = view;

            m_itemGenerator = itemGenerator;
        }

        /// <summary>
        /// Unique serial number
        /// </summary>
        public long SerialNumber { get; private set; }


        /// <summary>
        /// Price of the Item
        /// </summary>
        public long Price { get { return m_price; } }
        private long m_price;

        /// <summary>
        /// View of the selling item
        /// </summary>
        public Core.Game.Shopping.IShopItemView View { get; private set; }


        private Core.Game.Shopping.IItemGenerator m_itemGenerator;

        /// <summary>
        /// Generate real item instance
        /// </summary>
        /// <param name="worldState">Current world state</param>
        /// <returns>Generated item</returns>
        public Core.Game.Item Generate(Core.Interface.IWorldState worldState)
        {
            return m_itemGenerator.Generate(worldState);
        }


        #region Class Common

        static ShopItem()
        {
            ms_currentSerialNumber = 0;
        }

        #region Serial Number

        private static long ms_currentSerialNumber;

        private static long s_IssueSerialNumber()
        {
            return Interlocked.Increment(ref ms_currentSerialNumber);
        }

        #endregion

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Game
{
    /// <summary>
    /// The HOME
    /// </summary>
    internal class MyHome
    {
        public MyHome()
        {
            inventory = new List<Core.Game.Item>();
            m_arrivedItem = new List<Core.Game.Item>();
        }

        /// <summary>
        /// Items in the home
        /// </summary>
        public readonly List<Core.Game.Item> inventory;

        protected List<Core.Game.Item> m_arrivedItem;


        /// <summary>
        /// Refresh and collect newly arrived items
        /// </summary>
        /// <returns>List of newly arrived items</returns>
        public List<Core.Game.Item> RefreshAndGetArrivedItems()
        {
            if (m_arrivedItem.Count == 0)
            {
                return new List<Core.Game.Item>();
            }

            List<Core.Game.Item> collectedItems = m_arrivedItem;
            m_arrivedItem = new List<Core.Game.Item>();
            return collectedItems;
        }


        /// <summary>
        /// Deliver new item to the home. WOW!
        /// </summary>
        /// <param name="item">Arrived item</param>
        public void DeliverItem(Core.Game.Item item)
        {
            inventory.Add(item);
            m_arrivedItem.Add(item);
        }
    }
}

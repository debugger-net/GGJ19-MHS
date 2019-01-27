using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MHS.Game.Shop
{
    /// <summary>
    /// In-game shop object
    /// </summary>
    internal class Shop : Core.Game.Shopping.IShop
    {
        public Shop(List<ShopItem> shopItems, Core.Game.Shopping.IShopOrderProcessor orderProcessor)
        {
            SerialNumber = s_IssueSerialNumber();

            m_sellingItems = shopItems;
            m_orderProcessor = orderProcessor;

            m_processingOrders = new List<Core.Game.Shopping.Order>();

            m_workDays = new HashSet<DayOfWeek>()
                {
                    DayOfWeek.Monday,
                    DayOfWeek.Tuesday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Thursday,
                    DayOfWeek.Friday,
                };
        }

        /// <summary>
        /// Unique serial number
        /// </summary>
        public long SerialNumber { get; private set; }


        /// <summary>
        /// Get current selling catalogue
        /// </summary>
        public IEnumerable<Core.Game.Shopping.IShopItem> Catalogue
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Get current selling items
        /// </summary>
        internal IEnumerable<ShopItem> ConcreteCatalogue
        {
            get
            {
                return null;
            }
        }

        private List<ShopItem> m_sellingItems;


        private Core.Game.Shopping.IShopOrderProcessor  m_orderProcessor;
        private List<Core.Game.Shopping.Order>          m_processingOrders;

        private HashSet<DayOfWeek> m_workDays;


        public void UpdateStep(World world, DeliverySystem deliverySystem)
        {
            bool isSendingTime = false;
            if (m_workDays.Contains(world.LogicTime.DayOfWeek))
            {
                if (world.LogicTime.Hours == 17 && world.LogicTime.Minutes == 0)
                {
                    isSendingTime = true;
                }
            }

            List<Core.Game.Shopping.Order> sendingOrders = null;
            if (isSendingTime)
            {
                sendingOrders = new List<Core.Game.Shopping.Order>();
            }
            foreach (Core.Game.Shopping.Order currentOrder in m_processingOrders)
            {
                if (m_orderProcessor.UpdateOrder(currentOrder, world))
                {
                    if (isSendingTime)
                    {
                        sendingOrders.Add(currentOrder);
                    }
                }
                if (currentOrder.orderItem != null)
                {
                    // Update item here
                }
            }

            if (isSendingTime)
            {
                foreach (Core.Game.Shopping.Order currentOrder in m_processingOrders)
                {
                    currentOrder.orderState = Core.Game.Shopping.OrderState.kDelivering;
                    // deliverySystem send
                    m_processingOrders.Remove(currentOrder);
                }
            }
        }


        #region Class Common

        static Shop()
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

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MHS.Core.Game.Shopping
{
    /// <summary>
    /// State of Order
    /// </summary>
    public enum OrderState
    {
        kPlaced = 0, 
        kManufactoring = 1, 
        kWaitSending = 2, 
        kDelivering = 3, 
        kDelivered = 4, 
    }


    /// <summary>
    /// Item order
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Construct the order
        /// </summary>
        public Order(IShopItem orderedItem, Common.GameLogicTime time)
        {
            serialNumber = s_IssueSerialNumber();
            orderState = OrderState.kPlaced;
            placedTime = new Common.GameLogicTime(time.elapsedUnits);

            orderedShopItem = orderedItem;
            orderItem = null;
        }

        /// <summary>Unique serial number of the order</summary>
        public readonly long serialNumber;

        /// <summary>
        /// Order placed time
        /// </summary>
        public readonly Common.GameLogicTime placedTime;

        /// <summary>
        /// State of the order
        /// </summary>
        public OrderState orderState;

        /// <summary>
        /// Ordered shop selling item
        /// </summary>
        public readonly IShopItem orderedShopItem;

        /// <summary>
        /// Item of the order.
        /// Can be null before manufactored.
        /// </summary>
        public Item orderItem;


        #region Class Common

        static Order()
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

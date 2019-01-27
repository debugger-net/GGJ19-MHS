using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Contents.Shopping
{
    /// <summary>
    /// Default(testing) shop
    /// </summary>
    public class DefaultShopData : ShopData
    {
        /// <summary>
        /// Shop item cataluge data of the shop
        /// </summary>
        public override IEnumerable<ShopItemData> CatalogueData
        {
            get
            {
                List<ShopItemData> resultCatalogue = new List<ShopItemData>();

                resultCatalogue.Add(new BasicShopItemData(
                    100, 
                    new DummyItemGenerator(), 
                    new SimpleShopItemView("[SALE] for sale, baby shoes, never worn")
                    ));

                return resultCatalogue;
            }
        }


        /// <summary>
        /// Generate shop's order processor
        /// </summary>
        /// <returns>Created order processor</returns>
        public override Core.Game.Shopping.IShopOrderProcessor GenerateOrderProcessor()
        {
            return new _TempOrderProcessor();
        }


        internal class DummyItemGenerator : Core.Game.Shopping.IItemGenerator
        {
            public Core.Game.Item Generate(Core.Interface.IWorldState worldState)
            {
                return new Core.Game.Item(new Core.Game.Items.UnknownItemView());
            }
        }

        internal class SimpleShopItemView : Core.Game.Shopping.IShopItemView
        {
            public SimpleShopItemView(string name)
            {
                SellingItemName = name;
            }

            public string SellingItemName { get; private set; }
        }

        internal class _TempOrderProcessor : Core.Game.Shopping.IShopOrderProcessor
        {
            public _TempOrderProcessor()
            {
                m_manufactoringTable = new Dictionary<long, Core.Common.GameLogicTime>();
                m_workDays = new HashSet<DayOfWeek>()
                {
                    DayOfWeek.Monday,
                    DayOfWeek.Tuesday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Thursday,
                    DayOfWeek.Friday,
                };
            }

            private Dictionary<long, Core.Common.GameLogicTime> m_manufactoringTable;

            private HashSet<DayOfWeek> m_workDays;

            /// <summary>
            /// Update state of order and return is ready to deliver.
            /// </summary>
            /// <param name="order">Updating order</param>
            /// <param name="worldState">Current world state</param>
            /// <returns>Whether is ready to deliver</returns>
            public bool UpdateOrder(Core.Game.Shopping.Order order, Core.Interface.IWorldState worldState)
            {
                switch (order.orderState)
                {
                    case Core.Game.Shopping.OrderState.kPlaced:
                        if (m_workDays.Contains(worldState.LogicTime.DayOfWeek))
                        {
                            if (worldState.LogicTime.Hours >= 9 && worldState.LogicTime.Hours < 18)
                            {
                                order.orderState = Core.Game.Shopping.OrderState.kManufactoring;
                                m_manufactoringTable.Add(order.serialNumber,
                                    worldState.LogicTime + Core.Common.GameLogicTime.FromHours(28));
                            }
                        }
                        return false;

                    case Core.Game.Shopping.OrderState.kManufactoring:
                        bool isFinished = true;
                        if (m_manufactoringTable.ContainsKey(order.serialNumber))
                        {
                            if (m_manufactoringTable[order.serialNumber] <= worldState.LogicTime)
                            {
                                m_manufactoringTable.Remove(order.serialNumber);
                                order.orderItem = order.orderedShopItem.Generate(worldState);
                            }
                            else
                            {
                                isFinished = false;
                            }
                        }
                        if (isFinished)
                        {
                            if (worldState.LogicTime.Hours >= 9 && worldState.LogicTime.Hours < 18)
                            {
                                order.orderState = Core.Game.Shopping.OrderState.kWaitSending;
                                return true;
                            }
                        }
                        return false;
                }

                return true;
            }
        }
    }
}

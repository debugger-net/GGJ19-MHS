using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Game
{
    /// <summary>
    /// Item delivery system of a world
    /// </summary>
    internal class DeliverySystem
    {
        public DeliverySystem()
        {
            m_processingDeliveries = new List<Shop.Delivery>();

            m_workDays = new HashSet<DayOfWeek>()
                {
                    DayOfWeek.Monday,
                    DayOfWeek.Tuesday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Thursday,
                    DayOfWeek.Friday,
                    DayOfWeek.Saturday,
                };
        }

        private HashSet<DayOfWeek> m_workDays;

        public void RequestDeliver(World world, Core.Game.Shopping.Order deliveringOrder)
        {
            Shop.Delivery delivery = new Shop.Delivery();
            delivery.deliveryingOrder = deliveringOrder;
            delivery.deliveryDate = Core.Common.GameLogicTime.FromDays(world.LogicTime.TotalDays + 1);

            deliveringOrder.orderState = Core.Game.Shopping.OrderState.kDelivering;

            m_processingDeliveries.Add(delivery);
        }

        public void UpdateStep(World world, MyHome targetHome)
        {
            bool isDeliveryTime = false;
            if (m_workDays.Contains(world.LogicTime.DayOfWeek))
            {
                if (world.LogicTime.Hours == 14 && world.LogicTime.Minutes == 0)
                {
                    isDeliveryTime = true;
                }
            }

            List<Shop.Delivery> finishedDelivery = null;
            if (isDeliveryTime)
            {
                finishedDelivery = new List<Shop.Delivery>();
            }

            foreach (Shop.Delivery currentDelivery in m_processingDeliveries)
            {
                // Update Item here
                
                if (isDeliveryTime)
                {
                    if (currentDelivery.deliveryDate >= Core.Common.GameLogicTime.FromDays(world.LogicTime.TotalDays))
                    {
                        finishedDelivery.Add(currentDelivery);
                    }
                }
            }

            if (isDeliveryTime)
            {
                foreach (Shop.Delivery currentDelivery in finishedDelivery)
                {
                    currentDelivery.deliveryingOrder.orderState = Core.Game.Shopping.OrderState.kDelivered;
                    //Add Item to Inventory
                    m_processingDeliveries.Remove(currentDelivery);
                }
            }
        }

        private List<Shop.Delivery> m_processingDeliveries;
    }
}

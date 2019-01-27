using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Game.Shop
{
    /// <summary>
    /// Currently deliverying item
    /// </summary>
    internal class Delivery
    {
        public Core.Game.Shopping.Order     deliveryingOrder;
        public Core.Common.GameLogicTime    deliveryDate;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Core.Interface
{
    /// <summary>
    /// Shopping system state
    /// </summary>
    public interface IShoppingState
    {
        /// <summary>All shops avaliable</summary>
        IEnumerable<Game.Shopping.IShop> Shops { get; }


        /// <summary>List of all placed orders</summary>
        List<Game.Shopping.Order> OrderHistory { get; }
    }
}

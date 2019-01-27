using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Core.LogicCommands
{
    /// <summary>
    /// Type of detailed shopping command
    /// </summary>
    public enum ShoppingCommandType
    {
        kBuy = 1, 
    }


    /// <summary>
    /// Abstract base class of shopping commands
    /// </summary>
    public abstract class ShoppingCommand : LogicCommand
    {
        /// <summary>
        /// Coarse-level type of the command
        /// </summary>
        public override CommandCoarseType CoarseType { get { return CommandCoarseType.kShopping; } }

        /// <summary>
        /// Shopping command type
        /// </summary>
        public abstract ShoppingCommandType CommandType { get; }
    }


    /// <summary>
    /// Buy command
    /// </summary>
    public class ShoppingPurchaseCommand : ShoppingCommand
    {
        /// <summary>
        /// Shopping command type
        /// </summary>
        public override ShoppingCommandType CommandType { get { return ShoppingCommandType.kBuy; } }


        /// <summary>
        /// Shop Id
        /// </summary>
        public long purchasingShopId;

        /// <summary>
        /// Item Id
        /// </summary>
        public long purchasingItemId;
    }
}

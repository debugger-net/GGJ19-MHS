using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Game.Shop
{
    /// <summary>
    /// Shopping commnad logic processor
    /// </summary>
    internal class ShoppingCommandProcessor
    {
        /// <summary>
        /// Process a shopping command in the world
        /// </summary>
        /// <param name="world">Processing world</param>
        /// <param name="command">Command to process</param>
        /// <returns></returns>
        public Core.CommandResult ProcessCommand(World world, Core.LogicCommands.ShoppingCommand command)
        {
            switch (command.CommandType)
            {
                case Core.LogicCommands.ShoppingCommandType.kBuy:
                    Core.LogicCommands.ShoppingPurchaseCommand purchaseCommand = command as Core.LogicCommands.ShoppingPurchaseCommand;
                    ShopItem shopItem = world.Shoppings.FindShopItem(purchaseCommand.purchasingShopId, purchaseCommand.purchasingItemId);
                    if (shopItem == null)
                    {
                        return new Core.CommandResult(false, new CommandFailReason_NoSuchShopItem());
                    }

                    if (!world.Protagonist.SpendMoney((ulong)shopItem.Price))
                    {
                        return new Core.CommandResult(false, new CommandFailReason_NotEnoughMoney());
                    }

                    Core.Game.Shopping.Order placedOrder = world.Shoppings.DoOrder(purchaseCommand.purchasingShopId, shopItem, world);

                    return new Core.CommandResult(true, null, 
                        new PurchaseCommandResult() { placedOrder = placedOrder });
            }

            return new Core.CommandResult(false, new Processing.CommandFailReason_CannotProcess());
        }
        //
    }


    /// <summary>
    /// Purchasing command result
    /// </summary>
    public class PurchaseCommandResult : Core.ICommandResultData
    {
        public Core.Game.Shopping.Order placedOrder;
    }

    /// <summary>
    /// Command processing fail reason for not enough money
    /// </summary>
    public class CommandFailReason_NotEnoughMoney : Core.ICommandFailReason
    {
        /// <summary>
        /// Readable reason message
        /// </summary>
        /// <returns>Message string</returns>
        public override string Why() { return "Not enough money."; }
    }

    /// <summary>
    /// Command processing fail reason for invalid shop item given
    /// </summary>
    public class CommandFailReason_NoSuchShopItem : Core.ICommandFailReason
    {
        /// <summary>
        /// Readable reason message
        /// </summary>
        /// <returns>Message string</returns>
        public override string Why() { return "There is no item avaliable for selling."; }
    }
}

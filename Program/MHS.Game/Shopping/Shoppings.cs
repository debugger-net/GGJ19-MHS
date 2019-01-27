using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Game
{
    /// <summary>
    /// Shopping world object
    /// </summary>
    internal class Shoppings : Core.Interface.IShoppingState
    {
        public void UpdateStep(World world, DeliverySystem deliverySystem)
        {
            foreach (Shop.Shop currentShop in m_shops)
            {
                currentShop.UpdateStep(world, deliverySystem);
            }
        }

        public Shop.ShopItem FindShopItem(long shopId, long shopItemId)
        {
            if (!m_shopFindTable.ContainsKey(shopId))
            {
                return null;
            }

            Shop.Shop targetShop = m_shopFindTable[shopId];
            foreach (Shop.ShopItem findingItem in targetShop.ConcreteCatalogue)
            {
                if (findingItem.SerialNumber == shopItemId)
                {
                    return findingItem;
                }
            }

            return null;
        }

        public Core.Game.Shopping.Order DoOrder(long shopId, Shop.ShopItem purchasingItem, World world)
        {
            Core.Game.Shopping.Order order = new Core.Game.Shopping.Order(purchasingItem, world.LogicTime);
            m_shopFindTable[shopId].Order(order);
            m_orderHistory.Add(order);

            return order;


        }


        public Shoppings(IGameContext gameContext)
        {
            _InitializeShops(gameContext);
            m_orderHistory = new List<Core.Game.Shopping.Order>();
        }

        private List<Shop.Shop> m_shops;
        private Dictionary<long, Shop.Shop> m_shopFindTable;

        private void _InitializeShops(IGameContext gameContext)
        {
            m_shops = new List<Shop.Shop>();

            foreach (Contents.Shopping.ShopData currentShopData in gameContext.Data.InCodeData.ShopListData.allShopList)
            {
                List<Shop.ShopItem> items = new List<Shop.ShopItem>();
                foreach (Contents.Shopping.ShopItemData sellingItemData in currentShopData.CatalogueData)
                {
                    Shop.ShopItem currentItem = new Shop.ShopItem(
                        sellingItemData.Price, 
                        sellingItemData.View,
                        sellingItemData.ItemGenerator);
                    items.Add(currentItem);
                }

                m_shops.Add(new Shop.Shop(items, currentShopData.GenerateOrderProcessor()));
            }

            m_shopFindTable = new Dictionary<long, Shop.Shop>();
            foreach (Shop.Shop currentShop in m_shops)
            {
                m_shopFindTable.Add(currentShop.SerialNumber, currentShop);
            }
        }


        /// <summary>All shops avaliable</summary>
        public IEnumerable<Core.Game.Shopping.IShop> Shops { get { return m_shops; } }


        /// <summary>List of all placed orders</summary>
        public List<Core.Game.Shopping.Order> OrderHistory { get; }

        private List<Core.Game.Shopping.Order> m_orderHistory;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Game
{
    /// <summary>
    /// Shopping world object
    /// </summary>
    internal class Shoppings
    {
        public void UpdateStep(World world, DeliverySystem deliverySystem)
        {
            foreach (Shop.Shop currentShop in m_shops)
            {
                currentShop.UpdateStep(world, deliverySystem);
            }
        }


        public Shoppings(IGameContext gameContext)
        {
            _InitializeShops(gameContext);
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
    }
}

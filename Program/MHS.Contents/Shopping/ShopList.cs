using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Contents.Shopping
{
    /// <summary>
    /// List of providing shops
    /// </summary>
    public class ShopList
    {
        public ShopList()
        {
            allShopList = new List<ShopData>();
            allShopList.Add(new DefaultShopData());
        }

        public List<ShopData> allShopList;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Contents
{
    /// <summary>
    /// Coded Game Data Contetns
    /// </summary>
    public partial class CodedData
    {
        /// <summary>
        /// Load data in the code
        /// </summary>
        private void _LoadData()
        {
            _LoadBroadcastingData();
            _LoadShoppingData();
        }


        private void _LoadBroadcastingData()
        {
            PeopleData = new Broadcasting.People();
        }

        private void _LoadShoppingData()
        {
            ShopListData = new Shopping.ShopList();
        }
    }
}

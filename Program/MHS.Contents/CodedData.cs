﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Contents
{
    /// <summary>
    /// Coded Game Data Contetns
    /// </summary>
    public partial class CodedData
    {
        /// <summary>Data about people who are latent subscribers.</summary>
        public Broadcasting.People PeopleData { get; private set; }

        /// <summary>Data about shops.</summary>
        public Shopping.ShopList ShopListData { get; private set; }


        /// <summary>
        /// Create a coded game data object
        /// </summary>
        public CodedData()
        {
            _LoadData();
        }
    }
}

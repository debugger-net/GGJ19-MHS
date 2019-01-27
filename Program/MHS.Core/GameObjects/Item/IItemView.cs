using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Core.Game
{
    /// <summary>
    /// View data of an item for game client
    /// </summary>
    public interface IItemView
    {
        /// <summary>
        /// Showing name of the item
        /// </summary>
        string VisibleName { get; }

        /// <summary>
        /// Description of the item
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Detailed description of the item
        /// </summary>
        string DetailedDescription { get; }

        /// <summary>
        /// Short summary of the item
        /// </summary>
        string SummaryString { get; }
    }
}

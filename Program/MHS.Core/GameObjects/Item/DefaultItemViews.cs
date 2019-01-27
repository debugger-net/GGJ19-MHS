using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Core.Game.Items
{
    /// <summary>
    /// Default view of unknown item
    /// </summary>
    public class UnknownItemView : IItemView
    {
        /// <summary>
        /// Showing name of the item
        /// </summary>
        public string VisibleName { get { return "Unknown Item"; } }

        /// <summary>
        /// Description of the item
        /// </summary>
        public string Description { get { return "ad921d60486366258809553a3db49a4a"; } }

        /// <summary>
        /// Detailed description of the item
        /// </summary>
        public string DetailedDescription { get { return "ad921d60486366258809553a3db49a4as are continue to rotating..."; } }

        /// <summary>
        /// Short summary of the item
        /// </summary>
        public string SummaryString { get { return "Unknown Item - need to add view info"; } }
    }


    /// <summary>
    /// Default item view for item entity with breif component information
    /// </summary>
    public class ComponentBasedItemView : IItemView
    {
        /// <summary>
        /// Construct component based item view
        /// </summary>
        /// <param name="itemEntity">Viewing item entity</param>
        public ComponentBasedItemView(Item itemEntity)
        {
            m_itemEntity = itemEntity;
        }

        private readonly Item m_itemEntity;


        /// <summary>
        /// Showing name of the item
        /// </summary>
        public string VisibleName { get { return "Item Entity"; } }

        /// <summary>
        /// Description of the item
        /// </summary>
        public string Description
        {
            get
            {
                int componentCount = m_itemEntity._GetAllComponent().Count;
                return string.Format("Item entity with {0} component{1}",
                    componentCount, (componentCount > 1 ? "s" : ""));
            }
        }

        /// <summary>
        /// Detailed description of the item
        /// </summary>
        public string DetailedDescription
        {
            get
            {
                List<IItemComponent> components = m_itemEntity._GetAllComponent();

                StringBuilder descriptionBuilder = new StringBuilder();
                descriptionBuilder.Append("Item entity with ");
                descriptionBuilder.Append(components.Count);
                descriptionBuilder.Append(" component");
                descriptionBuilder.Append(components.Count > 1 ? "s" : "");

                foreach (IItemComponent currentComponent in components)
                {
                    descriptionBuilder.Append(Environment.NewLine);
                    descriptionBuilder.Append("  - ");
                    descriptionBuilder.Append(currentComponent.GetType().Name); 
                }

                return descriptionBuilder.ToString();
            }
        }

        /// <summary>
        /// Short summary of the item
        /// </summary>
        public string SummaryString
        {
            get
            {
                return string.Format("Item Entity (#.comp:{0})", m_itemEntity._GetAllComponent().Count);
            }
        }
    }
}

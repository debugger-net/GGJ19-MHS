using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;

namespace MHS.Core.Game
{
    /// <summary>
    /// Base class of game items
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Construct a basic item
        /// </summary>
        public Item(IItemView itemView)
        {
            serialNumber = s_IssueSerialNumber();
            View = itemView;

            _InitializeComponentSystem();
        }

        /// <summary>Unique serial number of the item</summary>
        public readonly long serialNumber;

        /// <summary>Client view data of the item</summary>
        public IItemView View { get; protected set; }


        #region Component System

        private object                                      m_componentsLock;
        private HashSet<IItemComponent>                     m_components;
        private ConcurrentDictionary<Type, IItemComponent>  m_componentTable;

        /// <summary>
        /// Get item component of given type
        /// </summary>
        /// <typeparam name="T">Component type</typeparam>
        /// <returns>Component of given type. null, if there is no component with the given type.</returns>
        public T GetComponent<T>() where T : class, IItemComponent
        {
            Type type = typeof(T);

            Type currentType = type;
            while (currentType != null && currentType != typeof(object))
            {
                if (m_componentTable.ContainsKey(currentType))
                {
                    return (m_componentTable[currentType] as T);
                }
            }

            Type[] interfaces = type.GetInterfaces();
            foreach (Type currentInterface in interfaces)
            {
                if (m_componentTable.ContainsKey(currentInterface))
                {
                    return (m_componentTable[currentInterface] as T);
                }
            }

            return null;
        }

        /// <summary>
        /// Register a component to the item
        /// </summary>
        /// <typeparam name="T">Type of component</typeparam>
        /// <param name="component">Item component</param>
        public void RegisterComponent<T>(T component) where T : class, IItemComponent
        {
            if (component == null)
            {
                return;
            }

            lock (m_componentsLock)
            {
                if (!m_components.Add(component))
                {
                    return;
                }

                Type type = typeof(T);

                Type currentType = type;
                while (currentType != null && currentType != typeof(object))
                {
                    m_componentTable[currentType] = component;
                    currentType = currentType.BaseType;
                }

                Type[] interfaces = type.GetInterfaces();
                foreach (Type currentInterface in interfaces)
                {
                    if (currentInterface != typeof(IItemComponent))
                    {
                        m_componentTable[currentInterface] = component;
                    }
                }
            }
        }

        /// <summary>
        /// Apply an action for all components
        /// </summary>
        /// <param name="action">Action to components</param>
        protected void _ForAllComponents(Action<IItemComponent> action)
        {
            List<IItemComponent> componentsList;
            lock (m_componentsLock)
            {
                componentsList = new List<IItemComponent>(m_components);
            }

            componentsList.ForEach(action);
        }

        /// <summary>
        /// Get all components of the item.
        /// Use only if knowing what to do.
        /// </summary>
        public List<IItemComponent> _GetAllComponent()
        {
            List<IItemComponent> componentsList;
            lock (m_componentsLock)
            {
                componentsList = new List<IItemComponent>(m_components);
            }

            return componentsList;
        }

        private void _InitializeComponentSystem()
        {
            m_componentsLock = new object();
            m_components = new HashSet<IItemComponent>();
            m_componentTable = new ConcurrentDictionary<Type, IItemComponent>();
        }

        #endregion


        #region Class Common

        static Item()
        {
            ms_currentSerialNumber = 0;
        }

        #region Serial Number

        private static long ms_currentSerialNumber;

        private static long s_IssueSerialNumber()
        {
            return Interlocked.Increment(ref ms_currentSerialNumber);
        }

        #endregion

        #endregion
    }
}

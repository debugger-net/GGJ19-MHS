using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Game
{
    /// <summary>
    /// Protagonist - the avatar of the player
    /// </summary>
    internal class Protagonist : Core.Interface.IProtagonistState
    {
        public Protagonist()
        {
            m_currentMoney = Contents.Meta.MetaConstants.kInitialMoney;
            TotalMoneyEarn = 0;
            MaximumMoney = CurrentMoney;

            m_healthState = 0.0;
            m_mentalState = 0.0;
        }


        public void UpdateStep()
        {

        }


        #region Logic Operations

        public bool GiveMoney(ulong amount)
        {
            CurrentMoney += (long)amount;
            TotalMoneyEarn += (long)amount;

            return true;
        }

        public bool SpendMoney(ulong amount)
        {
            if (CurrentMoney < (long)amount)
            {
                return false;
            }

            CurrentMoney -= (long)amount;

            return true;
        }

        #endregion


        #region State Interface

        /// <summary>Amount of money currently have.</summary>
        public long CurrentMoney
        {
            get { return m_currentMoney; }
            private set
            {
                m_currentMoney = value;

                if (value > MaximumMoney)
                {
                    MaximumMoney = value;
                }
            }
        }
        private long m_currentMoney;

        /// <summary>Total money earned in gameplay.</summary>
        public long TotalMoneyEarn { get; private set; }

        /// <summary>Maximum value of money have in history.</summary>
        public long MaximumMoney { get; private set; }


        /// <summary>Pyshical health state value of the protagonist. [-1.0, 1.0]</summary>
        public double HealthState
        {
            get { return m_healthState; }

            private set
            {
                if (value < -1.0)
                {
                    m_healthState = -1.0;
                }
                else if (value > 1.0)
                {
                    m_healthState = 1.0;
                }
                else
                {
                    m_healthState = value;
                }
            }
        }
        private double m_healthState;

        /// <summary>Mental state value of the protagonist. [-1.0, 1.0]</summary>
        public double MentalState
        {
            get { return m_mentalState; }

            private set
            {
                if (value < -1.0)
                {
                    m_mentalState = -1.0;
                }
                else if (value > 1.0)
                {
                    m_mentalState = 1.0;
                }
                else
                {
                    m_mentalState = value;
                }
            }
        }
        private double m_mentalState;


        /// <summary>
        /// Get Protagonist state for given sub-system
        /// </summary>
        /// <param name="subSystemId">Id of sub-system</param>
        /// <returns>Sub-system state</returns>
        public Core.SubSystem.ISubSystemProtagonistState GetSubSystemState(Core.SubSystemIds subSystemId)
        {
            return null;
        }

        #endregion
    }
}

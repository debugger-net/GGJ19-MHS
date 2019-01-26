using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Core.Interface
{
    /// <summary>
    /// Interface for state of a Protagonist
    /// </summary>
    public interface IProtagonistState
    {
        /// <summary>Amount of money currently have.</summary>
        long CurrentMoney { get; }

        /// <summary>Total money earned in gameplay.</summary>
        long TotalMoneyEarn { get; }

        /// <summary>Maximum value of money have in history.</summary>
        long MaximumMoney { get; }


        /// <summary>Pyshical health state value of the protagonist. [-1.0, 1.0]</summary>
        double HealthState { get; }

        /// <summary>Mental state value of the protagonist. [-1.0, 1.0]</summary>
        double MentalState { get; }


        /// <summary>
        /// Get Protagonist state for given sub-system
        /// </summary>
        /// <param name="subSystemId">Id of sub-system</param>
        /// <returns>Sub-system state</returns>
        SubSystem.ISubSystemProtagonistState GetSubSystemState(SubSystemIds subSystemId);
    }
}

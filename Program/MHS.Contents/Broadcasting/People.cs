using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Contents.Broadcasting
{
    /// <summary>
    /// People crowd data
    /// </summary>
    public class People
    {
        /// <summary>
        /// Create set list of populations
        /// </summary>
        /// <returns>List of populations</returns>
        public List<Core.Game.Population> CreateAllPopulationsSet()
        {
            List<Core.Game.Population> populations = new List<Core.Game.Population>()
            {
                new DefaultPopulation(),
            };

            return populations;
        }
    }
}

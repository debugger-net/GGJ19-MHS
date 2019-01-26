using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Game
{
    /// <summary>
    /// Crowd(= latent subscribers) system of a world
    /// </summary>
    internal class PeopleSystem
    {
        /// <summary>
        /// Effect of logic update result
        /// </summary>
        public struct UpdateEffect
        {
            public int retainedSubscriber;
            public int leavedSubscriber;

            public int SubscriberChange
            {
                get
                {
                    return retainedSubscriber - leavedSubscriber;
                }
            }
        }


        public UpdateEffect UpdateStep(MyChannel channel)
        {
            UpdateEffect updateResult = new UpdateEffect()
            {
                retainedSubscriber = 0, 
                leavedSubscriber = 0,
            };

            foreach (Core.Game.Population currentPopulation in m_populations)
            {
                Core.Game.Population.UpdateResult popuplationUpdateResult = currentPopulation.UpdateStep(channel);
                updateResult.retainedSubscriber += popuplationUpdateResult.newlyAddedSubscriber + popuplationUpdateResult.returnedSubscriber;
                updateResult.leavedSubscriber += popuplationUpdateResult.leavedSubscriber;
            }

            return updateResult;
        }


        public PeopleSystem(IGameContext gameContext)
        {
            m_populations = gameContext.Data.InCodeData.PeopleData.CreateAllPopulationsSet();
        }

        private List<Core.Game.Population> m_populations;
    }
}

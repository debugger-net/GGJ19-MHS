using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Contents.Broadcasting
{
    /// <summary>
    /// Default(testing) population
    /// </summary>
    public class DefaultPopulation : Core.Game.Population
    {
        /// <summary>
        /// Update process one logic step
        /// </summary>
        /// <param name="channelState">State of viewing channel</param>
        /// <returns>Update result</returns>
        public override UpdateResult UpdateStep(Core.Interface.IChannelState channelState)
        {
            UpdateResult result = new UpdateResult()
            {
                newlyAddedSubscriber = 0,
                returnedSubscriber = 0,
                leavedSubscriber = 0, 
            };

            if (channelState.CurrentStreaming == null)
            {
                ++m_channelOffStepCount;
            }
            else
            {
                m_channelOffStepCount = 0;

                result.newlyAddedSubscriber += 1;
                ++m_populationActiveSubscriber;
            }

            if (m_channelOffStepCount > 10)
            {
                if (m_populationActiveSubscriber > 0)
                {
                    result.leavedSubscriber += 1;
                    --m_populationActiveSubscriber;
                    ++m_populationLeavedSubscriber;
                }
                m_channelOffStepCount = 0;
            }

            return result;
        }


        public DefaultPopulation()
        {
            m_populationActiveSubscriber = 0;
            m_populationLeavedSubscriber = 0;

            m_channelOffStepCount = 0;
        }

        private int m_populationActiveSubscriber;
        private int m_populationLeavedSubscriber;

        private int m_channelOffStepCount;
    }
}

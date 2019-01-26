using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Core.Game
{
    /// <summary>
    /// Base class for crowd population (subscriber reservoir)
    /// </summary>
    public abstract class Population
    {
        /// <summary>
        /// Logic update effected result
        /// </summary>
        public struct UpdateResult
        {
            public int newlyAddedSubscriber;
            public int returnedSubscriber;
            public int leavedSubscriber;

            public int SubscriberChange
            {
                get
                {
                    return newlyAddedSubscriber + returnedSubscriber - leavedSubscriber;
                }
            }
        }

        /// <summary>
        /// Update process one logic step
        /// </summary>
        /// <param name="channelState">State of viewing channel</param>
        /// <returns>Update result</returns>
        public abstract UpdateResult UpdateStep(Core.Interface.IChannelState channelState);
    }
}

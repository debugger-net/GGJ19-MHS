using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Contents.Meta
{
    /// <summary>
    /// Class contains game-wide meta constants
    /// </summary>
    public class MetaConstants
    {
        /// <summary>Starting money</summary>
        public const long kInitialMoney = 1000000;


        /// <summary>Money-paying interval from broadcasting channel</summary>
        public static readonly Core.Common.GameLogicTime kChannelPayInterval = Core.Common.GameLogicTime.FromDays(1);

        /// <summary>Money-paying start time</summary>
        public static readonly Core.Common.GameLogicTime kChannelPayStart = Core.Common.GameLogicTime.FromDays(1);


        /// <summary>
        /// Calculate amount of money from the channel in one period
        /// </summary>
        /// <returns>Amount of money to earn</returns>
        public static long CalculatePayMoneyAmount(Core.Interface.IChannelState channelState)
        {
            return (channelState.SubscriberNumber * 10);
        }
    }
}

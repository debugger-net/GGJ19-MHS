using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Game.Processing
{
    /// <summary>
    /// Game state update processor
    /// </summary>
    internal class UpdateProcessor
    {
        /// <summary>
        /// Update world state one step
        /// </summary>
        /// <param name="targetWorld">Updating world</param>
        public void UpdateWorldOneStep(World targetWorld)
        {
            targetWorld.LogicTime.AddTime(Core.Common.GameLogicTime.Unit);

            targetWorld.Protagonist.UpdateStep();

            targetWorld.MyChannel.UpdateStep();
            PeopleSystem.UpdateEffect peopleSystemUpdateEffect = targetWorld.PeopleSystem.UpdateStep(targetWorld.MyChannel);
            targetWorld.MyChannel.ApplyPeopleResult(peopleSystemUpdateEffect);

            _UpdatePaying(targetWorld);
        }


        #region Logic

        private long _UpdatePaying(World targetWorld)
        {
            if (targetWorld.LogicTime < targetWorld.MetaLogicState.NextPayTime)
            {
                return 0;
            }

            long payingAmount = Contents.Meta.MetaConstants.CalculatePayMoneyAmount(targetWorld.MyChannel);

            targetWorld.Protagonist.GiveMoney((ulong)payingAmount);
            targetWorld.MetaLogicState.NextPayTime = targetWorld.LogicTime + Contents.Meta.MetaConstants.kChannelPayInterval;

            return payingAmount;
        }

        #endregion


        #region Internal

        internal UpdateProcessor(IGameContext gameContext)
        {
            m_gameContext = gameContext;
        }

        private readonly IGameContext m_gameContext;

        #endregion
    }
}

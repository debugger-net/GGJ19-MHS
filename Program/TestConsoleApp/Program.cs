using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MHS.Game.MHSGame gameInstance = MHS.Game.MHSGame.CreateNewGame();

            gameInstance.ProceedStep(MHS.Core.LogicCommand.IdleCommand);

            gameInstance.ProceedStep(new MHS.Game.Broadcasting.StartBroadcastingCommand(null));

            for (int i = 0; i < 1000; ++i)
            {
                gameInstance.ProceedStep(MHS.Core.LogicCommand.IdleCommand);
            }

            gameInstance.ProceedStep(new MHS.Game.Broadcasting.FinishBroadcastingCommand());

            for (int i = 0; i < 1000; ++i)
            {
                gameInstance.ProceedStep(MHS.Core.LogicCommand.IdleCommand);
            }

            Console.WriteLine("Finished");
        }
    }
}

using System.Text;

namespace TuringEmulator
{
    class Program
    {
        static void Main(string[] args)
        {
            TuringMachine machine = new TuringMachine();
            machine.Alphabet = "01";
            TransitionFunctionsTable table = new TransitionFunctionsTable
                (new[]
                {
                    new TransitionFunction(0,'0',0,'1',Directions.None),
                    new TransitionFunction(0,'0',0,'1',Directions.None),
                    new TransitionFunction(0,'0',1,'2',Directions.None),
                    new TransitionFunction(1,'0',0,'1',Directions.None),
                    new TransitionFunction(1,'1',0,'1',Directions.None)
                });
            foreach (var item in table)
            {
                Console.WriteLine(item);
            }
        }
    }
}
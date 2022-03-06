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
                    new TransitionFunction(0,'0',0,'1',Directions.Right),
                    new TransitionFunction(0,'1',0,'0',Directions.Right),
                    new TransitionFunction(0,' ',TuringMachine.HALT,' ',Directions.None),
                });
            machine.Tape = new InfTape("1010101", 0);
            Console.WriteLine(machine.Tape);
            machine.TFT = table;
            machine.Run();
            Console.WriteLine(machine.Tape);
        }
    }
}
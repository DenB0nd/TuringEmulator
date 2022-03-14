using System.Text;

namespace TuringEmulator
{
    static class Program
    {
        static void Main(string[] args)
        {
            TuringMachine machine = new();
            machine.Alphabet = " 01";
            machine.State = 0;
            machine.Tape = new InfiniteTape("0101010", 0);
            TransitionFunctionsTable table = new(
                new[]
                {
                    new TransitionFunction(0, '0', 0, '1', Directions.Right),
                    new TransitionFunction(0, '1', 0, '0', Directions.Right),
                    new TransitionFunction(0, ' ', TuringMachine.HALT, ' ', Directions.None)
                });
            machine.Table = table;

            machine.Run();

            Console.WriteLine(machine.Tape);
        }
    }
}
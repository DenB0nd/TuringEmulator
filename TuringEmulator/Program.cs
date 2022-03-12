using System.Text;

namespace TuringEmulator
{
    static class Program
    {
        static void Main(string[] args)
        {
            TuringMachine machine = new();
            machine.Alphabet = " 1";
            machine.State = 0;
            machine.Tape = new InfiniteTape("111 111", 0);
            TransitionFunctionsTable table = new(
                new[]
                {
                    new TransitionFunction(0, ' ', 0, ' ', Directions.Right),
                    new TransitionFunction(0, '1', 1, '1', Directions.Right),
                    new TransitionFunction(1, ' ', 2, '1', Directions.Right),
                    new TransitionFunction(1, '1', 1, '1', Directions.Right),
                    new TransitionFunction(2, ' ', 3, ' ', Directions.Left),
                    new TransitionFunction(2, '1', 2, '1', Directions.Right),
                    new TransitionFunction(3, ' ', 3, ' ', Directions.Left),
                    new TransitionFunction(3, '1', 4, ' ', Directions.Left),
                    new TransitionFunction(4, ' ', TuringMachine.HALT, ' ', Directions.None),
                    new TransitionFunction(4, '1', 4, '1', Directions.Left)
                });
            machine.Table = table;

            machine.Run();

            Console.WriteLine(machine.Tape);
        }
    }
}
using System.Text;

namespace TuringEmulator
{
    static class Program
    {
        static void Main(string[] args)
        {
            TuringMachine machine = new TuringMachine();
            machine.Alphabet = "01*";
            machine.Alphabet = " 1";
            machine.State = 0;
            Console.WriteLine(machine.Alphabet);
            TransitionFunctionsTable table = new TransitionFunctionsTable
                (new[]
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
            machine.Tape = new InfTape(" 11 11", 0);
            Console.WriteLine(machine.Tape);
            machine.Table = table;
            while(machine.State != TuringMachine.HALT)
            {
                machine.RunCommand();
                Console.WriteLine(machine.Tape);
            }
            Console.WriteLine(machine.Tape);
            
        }
    }
}
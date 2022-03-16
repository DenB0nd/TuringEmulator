using System.Diagnostics;
using System.Text;

namespace TuringEmulator
{
    static class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();

            TuringMachine machine = new();
            machine.Alphabet = "abcdefghijklmnopqrstuvwxyz";
            machine.State = 0;
            string tapeString = "abhhcewrkbapbslmxlzmbozfsmbinwarijcnvjznrbabaebczbeb";
            machine.Tape = new InfiniteTape(tapeString, 0);
            TransitionFunctionsTable table = new TransitionFunctionsTable(new[]
                {
                    new TransitionFunction(0, ' ', TuringMachine.HALT, ' ', Directions.None),
                    new TransitionFunction(1, ' ', 2, ' ', Directions.Left),
                    new TransitionFunction(2, ' ', 2, ' ', Directions.Left),
                    new TransitionFunction(2, 'h', 3, 'h', Directions.Right),
                    new TransitionFunction(3, ' ', 4, 'e', Directions.Right),
                    new TransitionFunction(4, ' ', 5, 'l', Directions.Right),
                    new TransitionFunction(5, ' ', 6, 'l', Directions.Right),
                    new TransitionFunction(6, ' ', TuringMachine.HALT, 'o', Directions.None),
                });

            foreach(var item in tapeString)
            {
                table.Add(new TransitionFunction(0, item, 1, 'h', Directions.Right));
                table.Add(new TransitionFunction(1, item, 1, ' ', Directions.Right));
            }

            machine.Table = table;

            while (machine.State != TuringMachine.HALT)
            {
                TransitionFunction tf = machine.Table.FindFunctionToPerformOrDefault(machine.Tape[machine.Head], machine.State);
                if (!machine.TryRunCommand(tf))
                {
                    break;
                }
                Console.WriteLine($"{machine.Tape} | {machine.Head}");
            }
            Console.WriteLine($"{machine.Tape} | {machine.Head}");
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            
        }
    }
}

// '1' * 400

// handmade cycle - first 123 then near 120
// handmade cycle add - first 115 then near 120
// machine run - first 115 then from 108 to 123
// machine run add - first 139 then near 120

// '1' * 1000

// handmade cycle - near 330
// machine run - near 330

// run/cycle doesn't matter?
// a little bit better not to use Add

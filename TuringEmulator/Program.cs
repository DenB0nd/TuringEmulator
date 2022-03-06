using System.Text;

namespace TuringEmulator
{
    class Program
    {
        static void Main(string[] args)
        {
            TuringMachine machine = new TuringMachine();
            machine.Alphabet = "01";
            machine.Alphabet = " 01";
            machine.State = 1;
            Console.WriteLine(machine.Alphabet);
            TransitionFunctionsTable table = new TransitionFunctionsTable
                (new[]
                {
                    new TransitionFunction(0,'0',0,'1',Directions.Right),
                    new TransitionFunction(0,'1',0,'0',Directions.Right),
                    new TransitionFunction(0,' ',0,'1',Directions.None),
                });
            machine.Tape = new InfTape("", 0);
            Console.WriteLine(machine.Tape);
            machine.TFT = table;
            while(machine.State != TuringMachine.HALT)
            {
                machine.RunCommand();
                Console.WriteLine(machine.Tape);
            }
            Console.WriteLine(machine.Tape);
        }
    }
}
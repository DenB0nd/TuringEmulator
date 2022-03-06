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
                    new TransitionFunction(0,'0',0,'1',Directions.None)

                });
            try
            {
                machine.CheckFunction(new TransitionFunction(0, '2', 0, '3', Directions.None));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            Console.WriteLine(new TransitionFunction(0, '2', 0, '3', Directions.None));
        }
    }
}
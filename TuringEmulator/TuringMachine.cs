namespace TuringEmulator
{
    public class TuringMachine
    {
        public const int HALT = -1;

        public int State { get; set; } = 0;
        public InfiniteTape Tape { get; set; } = InfiniteTape.Default;
        public int Head { get; set; } = 0;

        private string _alphabet = " ";
        public string Alphabet
        {
            get
            { 
                return _alphabet; 
            }
            set 
            {
                _alphabet = new string((value + " ")
                    .Distinct()
                    .ToArray());
            }
        } 
        public TransitionFunctionsTable Table { get; set; } = TransitionFunctionsTable.Default;

        public TuringMachine() { }

        public TuringMachine(TuringMachine machine)
        {
            ArgumentNullException.ThrowIfNull(machine);

            Tape = new InfiniteTape(machine.Tape);
            Alphabet = machine.Alphabet;
            State = machine.State;
            Head = machine.Head;
            Table = new TransitionFunctionsTable(machine.Table);
        }

        public TuringMachine Run()
        {          
            while (State != HALT)
            {
                TransitionFunction tf = Table.FindFunctionToPerformOrDefault(Tape[Head], State);
                if (!TryRunCommand(tf))
                {
                    break;
                }
            }
            return this;
        }

        public bool TryRunCommand(TransitionFunction tf)
        {    
            if (!CheckFunction(tf))
            {
                return false;
            }        
            MakeStep(tf);
            return true;
        }

        public void Reset()
        {
            Head = 0;
            State = 0;
        }

        private bool CheckFunction(TransitionFunction tf)
        {
            return true;       
        }

        private void MakeStep(TransitionFunction tf)
        {
            State = tf.NextState;
            Tape[Head] = tf.WriteSymbol;
            MoveHead(tf.Direction);
        }

        private void MoveHead(Directions direction) => Head += (int)direction;
    }
}
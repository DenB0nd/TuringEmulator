namespace TuringEmulator
{
    public class TuringMachine
    {
        public const int HALT = -1;

        public int State { get; set; } = 0;
        public InfiniteTape Tape { get; set; } = InfiniteTape.Default;
        public int Head { get; set; } = 0;

        private string alphabet = " ";
        public string Alphabet
        {
            get
            { 
                return alphabet; 
            }
            set 
            {
                alphabet = new string((value + " ")
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
                RunCommand();
            }
            return this;
        }

        public void RunCommand()
        {
            TransitionFunction tf = Table.FindFunctionToPerform(Tape[Head], State);
            CheckFunction(tf);
            MakeStep(tf);
        }

        public void Reset()
        {
            Head = 0;
            State = 0;
        }

        private void CheckFunction(TransitionFunction tf)
        {
            ArgumentNullException.ThrowIfNull(tf);

            if (tf == TransitionFunction.Default)
                TMThrowHelper.ThrowCommandException(Tape[Head], State);

            if (!Alphabet.Contains(tf.TapeSymbol))
                TMThrowHelper.ThrowAlphabetException(nameof(tf.TapeSymbol), tf.TapeSymbol);

            if (!Alphabet.Contains(tf.WriteSymbol))
                TMThrowHelper.ThrowAlphabetException(nameof(tf.WriteSymbol), tf.WriteSymbol);
       
        }

        private void MakeStep(TransitionFunction tf)
        {
            State = tf.NextState;
            Tape[Head] = tf.WriteSymbol;
            MoveHead(tf.Direction);
        }

        private void MoveHead(Directions direction) => Head += (int)direction;
    }

    static class TMThrowHelper
    {
        static public void ThrowAlphabetException(string name, char symbol)
        {
            throw new ArgumentException($"The Alphabet doesn't contain this {name} - \"{symbol}\"");
        }
        static public void ThrowCommandException(char symbol, int state)
        {
            throw new ArgumentException($"The table doesn't contain a command with the required parameters TapeSymbol - \"{symbol}\", State - {state}");
        }
    }
}
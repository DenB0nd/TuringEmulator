
namespace TuringEmulator
{
    enum Directions
    {
        Left = -1,
        None = 0,
        Right = 1
    }

    class TransitionFunction : IEquatable<TransitionFunction>
    {
        private int currentState;

        public int CurrentState
        {
            get { return currentState; }
            set 
            {
                if(value < -1)
                    currentState = -1;
                currentState = value;
            }
        }

        public char TapeSymbol { get; }

        private int nextState;
        
        public int NextState
        {
            get { return nextState; }
            set
            {
                if (value < -1)
                    nextState = -1;
                nextState = value;
            }
        }
        public char WriteSymbol { get; }

        public Directions Direction { get; }
        public TransitionFunction(int currentState = -1, char tapeSymbol = ' ', 
            int nextState = -1, char writeSymbol = ' ', Directions direction = Directions.None)
        {
            CurrentState = currentState;
            TapeSymbol = tapeSymbol;
            NextState = nextState;
            WriteSymbol = writeSymbol;
            Direction = direction;
        }

        static private readonly TransitionFunction _default = new TransitionFunction();
        static public TransitionFunction Default { get { return _default; } }

        public bool Equals(TransitionFunction? other)
        {
            return other is TransitionFunction && CurrentState == other.CurrentState && TapeSymbol == other.TapeSymbol && 
                NextState == other.NextState && WriteSymbol == other.WriteSymbol && Direction == other.Direction;    
        }

        public override string ToString() => CurrentState.ToString() + TapeSymbol +
            "->" + NextState.ToString() + WriteSymbol + Direction.ToString()[0]; 

    }


    class TransitionFunctionsTable
    {
        private List<TransitionFunction> transitionFunctions;

        public TransitionFunctionsTable(IEnumerable<TransitionFunction> collection)
        {
            if (collection == null)
                throw new ArgumentNullException();
            transitionFunctions = new List<TransitionFunction>(collection);
        }

        public TransitionFunctionsTable() => transitionFunctions = new List<TransitionFunction> ().Distinct().ToList();

        static private readonly TransitionFunctionsTable _default =
            new TransitionFunctionsTable(new List<TransitionFunction> { TransitionFunction.Default });

        static public TransitionFunctionsTable Default { get { return _default; } }

        public void Add(TransitionFunction tf) => transitionFunctions.Add(tf);

        public void Add(IEnumerable<TransitionFunction> tf) => transitionFunctions.AddRange(tf);

        public TransitionFunction FindFunction(char symbol, int state)
        {
            return transitionFunctions.FirstOrDefault(s => s.TapeSymbol == s.TapeSymbol && s.CurrentState == state) ?? TransitionFunction.Default;
        }


    }
}

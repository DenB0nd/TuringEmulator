
namespace TuringEmulator
{
    enum Directions
    {
        Left = -1,
        None = 0,
        Right = 1
    }

    class TransitionFunction
    {
        public int CurrentState { get; }
        public char TapeSymbol { get; }
        public int NextState { get; }
        public char WriteSymbol { get; }

        public Directions Direction { get; }
        public TransitionFunction(int currentState = 0, char tapeSymbol = ' ', 
            int nextState = 0, char writeSymbol = ' ', Directions direction = Directions.None)
        {
            CurrentState = currentState;
            TapeSymbol = tapeSymbol;
            NextState = nextState;
            WriteSymbol = writeSymbol;
            Direction = direction;
        }

        static private readonly TransitionFunction _default = new TransitionFunction();
        static public TransitionFunction Default { get { return _default; } }

    }


    class TransitionFunctionsTable
    {
        private List<TransitionFunction> transitionFunctions;

        public TransitionFunctionsTable(IEnumerable<TransitionFunction> collection)
        {
            if (collection == null)
                throw new ArgumentNullException();
            transitionFunctions = (List<TransitionFunction>) collection;
        }

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


using System.Collections;
using System.Linq;

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
                if (value < -1)
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


    class TransitionFunctionsTable : IEnumerable<TransitionFunction>
    {
        private List<TransitionFunction> transitionFunctions = new List<TransitionFunction>();

        public TransitionFunctionsTable(IEnumerable<TransitionFunction> collection) => Add(collection);

        public TransitionFunctionsTable() => transitionFunctions = new List<TransitionFunction>();

        static private readonly TransitionFunctionsTable _default =
            new TransitionFunctionsTable(new List<TransitionFunction> { TransitionFunction.Default });

        static public TransitionFunctionsTable Default { get { return _default; } }

        public void Add(TransitionFunction tf)
        {
            if (tf == null)
                throw new ArgumentNullException();
            transitionFunctions.Add(tf);
            RemoveCopies();
        }
        public void Add(IEnumerable<TransitionFunction> collection)
        {
            if (collection == null)
                throw new ArgumentNullException();
            transitionFunctions.AddRange(collection);
            RemoveCopies();
        }

        public TransitionFunction FindFunction(char symbol, int state)
        {
            return transitionFunctions.FirstOrDefault(s => s.TapeSymbol == s.TapeSymbol && s.CurrentState == state) ?? TransitionFunction.Default;
        }

        public IEnumerator<TransitionFunction> GetEnumerator() => transitionFunctions.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void RemoveCopies()
        {
            transitionFunctions = transitionFunctions
                .GroupBy(g => new
                {
                    g.CurrentState,
                    g.TapeSymbol
                })
                .Select(f => f.First())
                .ToList();
        }
    }
}

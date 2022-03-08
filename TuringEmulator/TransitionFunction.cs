
using System.Collections;

namespace TuringEmulator
{
    public enum Directions
    {
        Left = -1,
        None = 0,
        Right = 1
    }

    public sealed class TransitionFunction : IEquatable<TransitionFunction>
    {
        private int currentState;

        public int CurrentState
        {
            get { return currentState; }
            set
            {
                if (value < TuringMachine.HALT)
                    currentState = TuringMachine.HALT;

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
                if (value < TuringMachine.HALT)
                    nextState = TuringMachine.HALT;

                nextState = value;
            }
        }
        public char WriteSymbol { get; }

        public Directions Direction { get; }
        public TransitionFunction(int currentState = TuringMachine.HALT, char tapeSymbol = ' ',
            int nextState = TuringMachine.HALT, char writeSymbol = ' ', Directions direction = Directions.None)
        {
            CurrentState = currentState;
            TapeSymbol = tapeSymbol;
            NextState = nextState;
            WriteSymbol = writeSymbol;
            Direction = direction;
        }

        static private readonly TransitionFunction _default = new TransitionFunction();
        static public TransitionFunction Default { get { return _default; } }

        bool IEquatable<TransitionFunction>.Equals(TransitionFunction? other)
        {
            if(other == null)
                throw new ArgumentNullException(nameof(other));

            return CurrentState == other.CurrentState && TapeSymbol == other.TapeSymbol &&
                NextState == other.NextState && WriteSymbol == other.WriteSymbol && Direction == other.Direction;
        }

        public override string ToString() => CurrentState.ToString() + TapeSymbol +
            "->" + NextState.ToString() + WriteSymbol + Direction.ToString()[0];

    }


    public class TransitionFunctionsTable : IEnumerable<TransitionFunction>
    {
        private List<TransitionFunction> transitionFunctions = new List<TransitionFunction>();

        public TransitionFunctionsTable(IEnumerable<TransitionFunction> collection) => Add(collection);

        public TransitionFunctionsTable() => transitionFunctions = new List<TransitionFunction>();

        static private readonly TransitionFunctionsTable _default = new TransitionFunctionsTable();

        static public TransitionFunctionsTable Default { get { return _default; } }

        public void Add(TransitionFunction tf)
        {
            if (tf == null)
                throw new ArgumentNullException(nameof(tf));

            transitionFunctions.Add(tf);

            RemoveCopies();
        }
        public void Add(IEnumerable<TransitionFunction> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            transitionFunctions.AddRange(collection);

            RemoveCopies();
        }

        public TransitionFunction FindFunctionToPerform(char symbol, int state)
        {
            return transitionFunctions.FirstOrDefault(s => s.TapeSymbol == symbol && s.CurrentState == state) ?? TransitionFunction.Default;
        }

        public IEnumerator<TransitionFunction> GetEnumerator() => transitionFunctions.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void RemoveCopies()
        {
            transitionFunctions = transitionFunctions
                .Where(s => s != null)
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

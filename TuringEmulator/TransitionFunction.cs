
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
            set { currentState = ConvertToCommonState(value); }
        }

        public char TapeSymbol { get; }

        private int nextState;

        public int NextState
        {
            get { return nextState; }
            set { nextState = ConvertToCommonState(value); }
        }

        public char WriteSymbol { get; }

        public Directions Direction { get; }

        static private readonly TransitionFunction _default = new TransitionFunction();

        static public TransitionFunction Default { get { return _default; } }

        public TransitionFunction(int currentState = TuringMachine.HALT, char tapeSymbol = ' ',
            int nextState = TuringMachine.HALT, char writeSymbol = ' ', Directions direction = Directions.None)
        {
            CurrentState = currentState;
            TapeSymbol = tapeSymbol;
            NextState = nextState;
            WriteSymbol = writeSymbol;
            Direction = direction;
        }

 
        bool IEquatable<TransitionFunction>.Equals(TransitionFunction? other)
        {
            ArgumentNullException.ThrowIfNull(other);

            return CurrentState == other.CurrentState && TapeSymbol == other.TapeSymbol &&
                NextState == other.NextState && WriteSymbol == other.WriteSymbol && Direction == other.Direction;
        }

        public override string ToString() => CurrentState.ToString() + TapeSymbol +
            "->" + NextState.ToString() + WriteSymbol + Direction.ToString()[0];

        private int ConvertToCommonState(int state) => state < 0 ? -1 : state;

    }


    public class TransitionFunctionsTable : IEnumerable<TransitionFunction>
    {

        private List<TransitionFunction> transitionFunctions = new();

        static private readonly TransitionFunctionsTable _default = new();

        static public TransitionFunctionsTable Default { get { return _default; } }

        public TransitionFunctionsTable() { }

        public TransitionFunctionsTable(IEnumerable<TransitionFunction> collection) => Add(collection);

        public TransitionFunctionsTable(TransitionFunctionsTable table)
        {
            ArgumentNullException.ThrowIfNull(table);

            transitionFunctions = new List<TransitionFunction>(table.transitionFunctions);
        }

        public IEnumerator<TransitionFunction> GetEnumerator() => transitionFunctions.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(TransitionFunction tf)
        {
            ArgumentNullException.ThrowIfNull(tf);

            transitionFunctions.Add(tf);

            RemoveCopies();
        }

        public void Add(IEnumerable<TransitionFunction> collection)
        {
            ArgumentNullException.ThrowIfNull(collection);

            transitionFunctions.AddRange(collection);

            RemoveCopies();
        }

        public TransitionFunction FindFunctionToPerform(char symbol, int state)
        {
            return transitionFunctions.FirstOrDefault(s => s.TapeSymbol == symbol && s.CurrentState == state) ?? TransitionFunction.Default;
        }

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

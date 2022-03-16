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

        static public TransitionFunction Default => new();

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
}

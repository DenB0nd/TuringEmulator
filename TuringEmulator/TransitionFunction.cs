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
        private int _currentState;

        public int CurrentState
        {
            get { return _currentState; }
            set { _currentState = ConvertToCommonState(value); }
        }

        public char TapeSymbol { get; set; }

        private int _nextState;

        public int NextState
        {
            get { return _nextState; }
            set { _nextState = ConvertToCommonState(value); }
        }

        public char WriteSymbol { get; set; }

        public Directions Direction { get; set; }

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

        public override string ToString() => $"{CurrentState}{TapeSymbol}->{NextState }{WriteSymbol}{Direction}";

        private int ConvertToCommonState(int state) => state < 0 ? TuringMachine.HALT : state;

    }
}

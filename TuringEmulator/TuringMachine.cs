using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringEmulator
{
    class InfTape : IEnumerable<char>
    {
        private StringBuilder Tape { get; } = new("");
        private int Origin { get; set; } = 0;

        public InfTape(string str, int origin) => Set(str, origin);
        public InfTape(StringBuilder str, int origin) => Set(str.ToString(), origin);

        public void Set(string str, int origin)
        {
            Clear();
            Tape.Append(str);
            Origin = origin;
        }

        public void Clear() => Tape.Clear();

        static private readonly InfTape _default = new InfTape(" ", 0);
        static public InfTape Default { get { return _default; } }

        public char this[int index]
        {
            get => Tape[Origin + index];
            set => Tape[Origin + index] = value;
        }

        public override string ToString() => Tape.ToString();

        public IEnumerator<char> GetEnumerator()
        {
            for(int i = 0; i < Tape.Length; i++)
            {
                yield return Tape[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    enum Directions
    {
        Left,
        Right,
        None
    }

    class TransitionFunction
    {
        public int CurrentState { get; }
        public char TapeSymbol { get; }
        public int NextState { get; }
        public char WriteSymbol { get; }

        public Directions direction;
        public TransitionFunction(int currentState, char tapeSymbol, int nextState, char writeSymbol, Directions direction = default)
        {
            CurrentState = currentState;
            TapeSymbol = tapeSymbol;
            NextState = nextState;
            WriteSymbol = writeSymbol;
            this.direction = direction;
        }

        static private readonly TransitionFunction _default = new TransitionFunction(0, ' ', 0, ' ', Directions.None);

        static public TransitionFunction Default { get { return _default; } }

    }


    class TransitionFunctionsTable
    {
        private IList<TransitionFunction> transitionFunctions;

        public TransitionFunctionsTable(IList<TransitionFunction> transitionFunctions)
        {
            this.transitionFunctions = transitionFunctions;
        }

        static private readonly TransitionFunctionsTable _default =
            new TransitionFunctionsTable(new List<TransitionFunction> { TransitionFunction.Default });

        static public TransitionFunctionsTable Default { get { return _default; } }

        public void AddTransitionFunction(TransitionFunction tf)
        {
            transitionFunctions.Add(tf);
        }
    }

    internal class TuringMachine
    {
        public const int HALT = -1;

        public int State { get; } = 0;
        public InfTape Tape { get; set; } = InfTape.Default;
        public int Head { get; set; } = 0;
        public string Alphabet { get; set; } = " ";
        public TransitionFunctionsTable TransitionFunctionsTable { get; set; } = TransitionFunctionsTable.Default;

        public TuringMachine(TransitionFunctionsTable transitionFunctionsTable, InfTape tape, string alphabet = " ")
        {
            TransitionFunctionsTable = transitionFunctionsTable;
            Tape = tape;
            Alphabet = alphabet;
        }

        public TuringMachine() : this(TransitionFunctionsTable.Default, InfTape.Default) { }


    }
}

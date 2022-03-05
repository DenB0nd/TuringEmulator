using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringEmulator
{

    internal class TuringMachine
    {
        public const int HALT = -1;

        public int State { get; set; } = 0;
        public InfTape Tape { get; set; } = InfTape.Default;
        public int Head { get; set; } = 0;
        public string Alphabet { get; set; } = " ";
        public TransitionFunctionsTable TransitionFunctionsTable { get; set; } = TransitionFunctionsTable.Default;

        private TransitionFunction CurrentFunction { get; set; } = null;

        public TuringMachine(TransitionFunctionsTable transitionFunctionsTable, InfTape tape, string alphabet = " ")
        {
            TransitionFunctionsTable = transitionFunctionsTable;
            Tape = tape;
            Alphabet = alphabet;
            CurrentFunction = TransitionFunctionsTable.FindFunction(Tape[Head], State);
        }
        public TuringMachine() : this(TransitionFunctionsTable.Default, InfTape.Default) { }

        public bool Run()
        {
            return true;
        }

        private void RunCommand(TransitionFunction tf)
        {
            if (!CheckFunction(tf))
                return;       
            
        }

        public void MakeStep(TransitionFunction tf)
        {
            State = tf.NextState;
            Tape[Head] = tf.WriteSymbol;
            Move(tf.Direction);
        }

        private void Move(Directions direction) => Head += (int)direction;

        public bool Check()
        {
            return true;
        }

        public bool CheckFunction(TransitionFunction tf)
        {
            return true;
        }

        

    }
}

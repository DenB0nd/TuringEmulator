using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringEmulator
{
    internal class TuringMachine
    {
        static public readonly int HALT = -1;

        public int State { get; set; } = 0;
        public InfTape Tape { get; set; } = InfTape.Default;
        public int Head { get; set; } = 0;
        public string Alphabet { get; set; } = " ";
        public TransitionFunctionsTable TFT { get; set; } = TransitionFunctionsTable.Default;

        //private TransitionFunction CurrentFunction { get; set; };

        public void Clear() => Tape.Clear();

        public TuringMachine(TransitionFunctionsTable table, InfTape tape)
        {
            TFT = table;
            Tape = tape;
        }

        public TuringMachine() : this(TransitionFunctionsTable.Default, InfTape.Default) { }

        public bool Run()
        {          
            while (State != HALT)
            {
                RunCommand();
            }
            return true;
        }

        private void RunCommand()
        {
            TransitionFunction tf = TFT.FindFunction(Tape[Head], State);
            MakeStep(tf);
        }

        public void MakeStep(TransitionFunction tf)
        {
            State = tf.NextState;
            Tape[Head] = tf.WriteSymbol;
            Move(tf.Direction);
        }

        private void Move(Directions direction) => Head += (int)direction;

        public void Reset()
        {
            Head = 0;
            State = 0;
        }

        public bool Check()
        {
            return true;
        }

        public void CheckFunction(TransitionFunction tf)
        {
            if (!Alphabet.Contains(tf.TapeSymbol))
                TMThrowHelper.ThrowAlphabetException(nameof(tf.TapeSymbol), tf.TapeSymbol);
            if (!Alphabet.Contains(tf.WriteSymbol))
                TMThrowHelper.ThrowAlphabetException(nameof(tf.WriteSymbol), tf.WriteSymbol);
            
        }
    }

    static class TMThrowHelper
    {
        static public void ThrowAlphabetException(string name, char symbol)
        {
            throw new ArgumentException($"The Alphabet doesn't contain this {name} - {symbol}");
        }
    }
}
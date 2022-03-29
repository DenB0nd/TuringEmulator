using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TuringEmulator;

namespace TuringMachineAppWPF
{
    internal class TuringMachineModel
    {
        private readonly TuringMachine turingMachine = new();

        

        public string Tape { get; set; } = "";
        public int Head { get; set; } = 0;
        public int State { get; set; } = 0;
        public TransitionFunctionsTable Table { get; set; } = new TransitionFunctionsTable();

        public void MakeStep()
        {
            turingMachine.Tape = new InfiniteTape(Tape);
            turingMachine.Table = Table;
            turingMachine.Head = Head;
            TransitionFunction transitionFunction = turingMachine.Table.FindFunctionToPerformOrDefault(Tape[Head], State);
            if (!transitionFunction.Equals(TransitionFunction.Default))
            {
                turingMachine.MakeStep(transitionFunction);
            }

            Tape = turingMachine.Tape.ToString();
            Head = turingMachine.Head;
            State = turingMachine.State;

        }

    }

    internal class TransitionFunctionModel
    {
        

        public int CurrentState { get; set; }

        private string _tapeSymbol = " ";

        public string TapeSymbol 
        {
            get => $"\"{_tapeSymbol}\"";
            set => _tapeSymbol = value[0].ToString(); 
        }

        public int NextState { get; set; }

        private string _writeSymbol = " ";
        
        public string WriteSymbol
        {
            get => $"\"{_writeSymbol}\"";
            set => _writeSymbol = value[0].ToString();
        }

        public Directions Direction { get; set; }

        static public TransitionFunctionModel Default => new();

        public TransitionFunctionModel(int currentState = TuringMachine.HALT, string tapeSymbol = " ",
            int nextState = TuringMachine.HALT, string writeSymbol = " ", Directions direction = Directions.None)
        {
            CurrentState = currentState;
            TapeSymbol = tapeSymbol[0].ToString();
            NextState = nextState;
            WriteSymbol = writeSymbol[0].ToString();
            Direction = direction;
        }

        public TransitionFunction GetFunction()
        {
            return new TransitionFunction(CurrentState, TapeSymbol[1], NextState, WriteSymbol[1], Direction);
        }
    }
}

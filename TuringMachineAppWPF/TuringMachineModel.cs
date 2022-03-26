using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringEmulator;

namespace TuringMachineAppWPF
{
    internal class TuringMachineModel
    {
        private readonly TuringMachine turingMachine = new();

        

        public string Tape { get; set; } = "";

        public void MakeStep()
        {

        }

        public void Set()
        {
            machine.Tape = new InfiniteTape(Tape);
            //machine.TryRunCommand();
        }
    }

    internal class TransitionFunctionModel
    {
        

        public int CurrentState { get; set; }

        private string _tapeSymbol;

        public string TapeSymbol 
        {
            get => $"\" {_tapeSymbol} \"";
            set => _tapeSymbol = value[0].ToString(); 
        }

        public int NextState { get; set; }

        private string _writeSymbol;
        
        public string WriteSymbol
        {
            get => $"\" {_writeSymbol} \"";
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
    }
}

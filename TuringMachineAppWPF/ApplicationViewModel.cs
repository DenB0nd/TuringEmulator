using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TuringEmulator;

namespace TuringMachineAppWPF
{
    internal class ApplicationViewModel : BindableBase, INotifyPropertyChanged
    {
        public LinkedList<TransitionFunctionModel> TransitionFunctionsModel { get; set; } = new LinkedList<TransitionFunctionModel>(new []{ TransitionFunctionModel.Default });

        private TuringMachineModel machine = new();
        public TuringMachineModel Machine
        {
            get
            {
                machine.Tape = Tape;
                return machine;
            }
        }

        public string StateText
        {
            get
            {
                return new string(Center.ToString());
            }
        }

        private string tape = "";
        public string Tape
        {
            get
            {
                return tape;
            }
            set
            {
                SetProperty(ref tape, value);
                RaisePropertyChanged(nameof(Machine));
            }
        }

        private int center = 0;
        public int Center
        {
            get
            {
                return center;
            }
            set
            {
                SetProperty(ref center, value);
                RaisePropertyChanged(nameof(StateText));

            }
        }



        public ApplicationViewModel()
        {
            SetCommands();

        }
        private void SetCommands()
        {
            VisiblePartToRightCommand = new DelegateCommand(() =>
            {
                Center++;
            });
            VisiblePartToLeftCommand = new DelegateCommand(() =>
            {
                Center--;
                //MessageBox.Show("Left");
            });
            MakeStep = new DelegateCommand(() =>
            {
                machine.MakeStep();
            });
        }

        public DelegateCommand MakeStep { get; set; }
        public DelegateCommand VisiblePartToRightCommand { get; set; }

        public DelegateCommand VisiblePartToLeftCommand { get; set; }

    }
}

using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TuringEmulator;

namespace TuringMachineAppWPF
{
    internal class ApplicationViewModel : BindableBase
    { 
        private TuringMachine turingMachine = new TuringMachine();
        public VisibleTape VisibleTapeModel { get; set; }

        public string StateText => new string(center.ToString());

        private ObservableCollection<Cells> visibleTape = new ObservableCollection<Cells>(new[] { new Cells('2'), new Cells('3'), new Cells('4') });
        public ObservableCollection<Cells> VisibleTape
        { 
            get
            {
                return visibleTape;
            }
            set
            {
                SetProperty(ref visibleTape, value);
            }
        }

        private int center = 0;
        public int Center
        {
            get { return center; }
            set
            {
                SetProperty(ref center, value);
                RaisePropertyChanged(nameof(StateText));
                RaisePropertyChanged(nameof(VisibleTape));
            }
        }



        public ApplicationViewModel()
        {
            SetCommands();

           
        }

        public TuringMachine TuringMachine
        {
            get { return turingMachine; }
            set { turingMachine = value; }
        }

        private void SetCommands()
        {
            VisiblePartToRightCommand = new DelegateCommand(() =>
            {
                Center++;
                //MessageBox.Show("Right");
            });
            VisiblePartToLeftCommand = new DelegateCommand(() =>
            {
                Center--;
                //MessageBox.Show("Left");
            });
        }

        public DelegateCommand VisiblePartToRightCommand { get; set; }

        public DelegateCommand VisiblePartToLeftCommand { get; set; }

    }

    class Cells
    {
        public char Cell { get; set; }

        public Cells(char cell)
        {
            Cell = cell;
        }
    }
}

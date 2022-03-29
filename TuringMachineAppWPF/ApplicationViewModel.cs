using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace TuringMachineAppWPF
{
    internal class ApplicationViewModel : BindableBase, INotifyPropertyChanged
    {
        public ObservableCollection<TransitionFunctionModel> DataGridTransitionFunctions { get; set; } = new ObservableCollection<TransitionFunctionModel>(new []{ TransitionFunctionModel.Default });

        private TuringMachineModel _machine = new();

        private int _head;

        public int Head
        {
            get { return _head; }
            set 
            {
                SetProperty(ref _head, value);
                RaisePropertyChanged(nameof(HeadText));
            }
        }

        private int _state = 0;

        public int State
        {
            get { return _state; }
            set
            {
                SetProperty(ref _state, value);
                RaisePropertyChanged(nameof(StateText));
            }
        }

        public string StateText
        {
            get
            {
                return $"State: {_state}";
            }
        }

        public string HeadText
        {
            get
            {
                return $"Head: {_head}";
            }
        }

        private string _apllicationTape = "";
        public string ApplicationTape
        {
            get
            {
                return _apllicationTape;
            }
            set
            {
                SetProperty(ref _apllicationTape, value);
            }
        }


        public ApplicationViewModel() => SetCommands();
        private void SetCommands()
        {
            MakeStep = new DelegateCommand(() =>
            {
                if (_state != -1)
                {
                    _machine.Tape = ApplicationTape;
                    _machine.Table.Add(DataGridTransitionFunctions.Select(x => x.GetFunction()));
                    _machine.Head = _head;
                    _machine.State = _state;
                    _machine.MakeStep();
                    State = _machine.State;
                    Head = _machine.Head;
                    ApplicationTape = _machine.Tape;
                }
                MessageBox.Show($"{ApplicationTape} {_head} {_state}");
            });
            AddCommand = new DelegateCommand(() =>
            {
                DataGridTransitionFunctions.Add(TransitionFunctionModel.Default);
            });
        }

        public DelegateCommand MakeStep { get; set; }
        public DelegateCommand AddCommand { get; set; }


    }
}


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringEmulator;

namespace TuringMachineAppWPF
{
    internal class VisibleTape
    {
        public const int VISIBLE_PART_SIZE = 25;

        private InfiniteTape _finiteTape = new InfiniteTape();
        public InfiniteTape InfiniteTape
        { 
            get
            {
                return _finiteTape;
            }
            private set
            {
                for (int i = LeftBorderOfVisiblePart(); i <= RightBorderOfVisiblePart(); i++)
                {
                    InfiniteTape[i] = VisiblePart[i - VISIBLE_PART_SIZE / 2];
                }
            }
        }

        public int CenterOfVisiblePart { get; set; } = 0;

        private ObservableCollection<char> _visiblePart = new ObservableCollection<char>();
        public ObservableCollection<char> VisiblePart;

        public VisibleTape()
        {
            VisiblePart = new ObservableCollection<char>(_visiblePart);
        }

        public VisibleTape(int head) : this()
        {
            CenterOfVisiblePart = head;
        }

        public void ChangeVisiblePart(char[] array)
        {
            _visiblePart = new ObservableCollection<char>(array);
        }


        private int LeftBorderOfVisiblePart() => CenterOfVisiblePart - VISIBLE_PART_SIZE / 2;
        private int RightBorderOfVisiblePart() => CenterOfVisiblePart + VISIBLE_PART_SIZE / 2;
    }
}

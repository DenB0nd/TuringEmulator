using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TuringMachineAppWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<TextBlock> TapeTextBlockArray = new List<TextBlock>();
        public MainWindow()
        {
            InitializeComponent();
            
        }

        /*private void CreateTextBoxTape()
        {
            for (int i = 0; i < 25; i++)
            {
                TextBlock t = new TextBlock();
                t.Text = Convert.ToString(i - 12);
                TapeTextBlockArray.Add(t);
                t.Background = new SolidColorBrush(Colors.White);
                Grid.SetColumn(t, i + 1);
                Grid.SetRow(t, 1);
                TapeGrid.Children.Add(TapeTextBlockArray[i]);
            }
        }*/


    }
}

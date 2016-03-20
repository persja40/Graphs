using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace Graphs.Windows.Project2
{
    /// <summary>
    /// Interaction logic for IsGraphical.xaml
    /// </summary>
    public partial class IsGraphical : Window
    {
        public ObservableCollection<int> Degrees { get; set; } = new ObservableCollection<int>();
        public IsGraphical()
        {
            InitializeComponent();

            Degrees.Add(1);
            Degrees.Add(1);

            DataContext = this;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            if(Validation.GetHasError(DegreesTextBox))
            {
                MessageBox.Show("You have errors!");
            }
            else
            this.Close();
        }
    }
}

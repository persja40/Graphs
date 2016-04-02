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
using System.Windows.Shapes;

namespace Graphs.Windows.Project3
{
    /// <summary>
    /// Interaction logic for SelectWeightWindow.xaml
    /// </summary>
    public partial class SelectWeightWindow : Window
    {

        public int Weight { get; set; }
        public SelectWeightWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

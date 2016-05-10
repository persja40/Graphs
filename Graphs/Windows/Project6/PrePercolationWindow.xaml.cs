using Graphs.Data;
using Graphs.ViewModels;
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

namespace Graphs.Windows.Project6
{
    /// <summary>
    /// Interaction logic for PrePercolationWindow.xaml
    /// </summary>
    public partial class PrePercolationWindow : Window
    {
        public int Nodes { get; set; } = 7;
        public int Prop { get; set; } = 50;

        public PrePercolationWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        private void Generate(object sender, RoutedEventArgs e)
        {
            PerlocationViewModel vm = new PerlocationViewModel(new Percolation(Nodes, Prop / 100.0));
            PercolationWindow wind = new PercolationWindow(vm);
            wind.ShowDialog();
        }
    }
}

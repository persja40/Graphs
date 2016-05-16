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
    /// Interaction logic for Dijkstra.xaml
    /// </summary>
    /// 
    public partial class Dijkstra : Window
    {
        public int StartNode { get; set; }
        public int EndNode { get; set; }

        public Dijkstra()
        {
            InitializeComponent();

            DataContext = this;
        }

        public Dijkstra(bool hideEndNode = true) : this()
        {
            if (hideEndNode)
                EndNodePanel.Visibility = Visibility.Collapsed;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

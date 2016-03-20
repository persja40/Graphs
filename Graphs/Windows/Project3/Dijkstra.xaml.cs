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
        public int StartNode
        {
            get
            {
                return int.Parse(StartNodeTextBox.Text);
            }
        } 

        public int EndNode
        {
            get
            {
                return int.Parse(EndNodeTextBox.Text);
            }
        }


        public Dijkstra()
        {
            InitializeComponent();
        }
    }
}

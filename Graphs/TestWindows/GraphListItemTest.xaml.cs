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

namespace Graphs.TestWindows
{
    /// <summary>
    /// Interaction logic for GraphListItemTest.xaml
    /// </summary>
    public partial class GraphListItemTest : Window
    {
        public GraphListItemViewModel Model { get; set; }
        public GraphListItemTest()
        {
            InitializeComponent();

            Model = new GraphListItemViewModel()
            {
                NodeNumer = 69
            };

            for(int i = 0;i < 22; ++i)
            {
                Model.ConnectedNodes.Add(i * 2);
            }

            Control1.DataContext = Model;

        }
    }
}

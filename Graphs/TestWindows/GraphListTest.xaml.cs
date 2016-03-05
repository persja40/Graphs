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
    /// Interaction logic for GraphListTest.xaml
    /// </summary>
    public partial class GraphListTest : Window
    {
        GraphListViewModel Model { get; set; }
        public GraphListTest()
        {
            InitializeComponent();
            Model = new GraphListViewModel();

            for(int i = 0;i < 10;++i)
            {
                var model = new GraphListItemViewModel()
                {
                    NodeNumer = i * 3
                };

                for(int j = 0;j < i*2;++j)
                {
                    model.ConnectedNodes.Add(i/3 + j);
                }

                Model.Items.Add(model);
            }

            Control1.DataContext = Model;
        }
    }
}

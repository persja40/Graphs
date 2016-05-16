using Graphs.Actions;
using Graphs.Data;
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

namespace Graphs.Windows.Project5
{
    /// <summary>
    /// Interaction logic for CreateFlowNetworkWindow.xaml
    /// </summary>
    public partial class CreateFlowNetworkWindow : Window
    {
        public CreateFlowNetworkWindow(bool skojarzenie = false)
        {
            InitializeComponent();

            if(skojarzenie)
            {
                Row1.Height = new GridLength(0);
                Row2.Height = new GridLength(0);
            }
            else
            {
                Row3.Height = new GridLength(0);
                Row4.Height = new GridLength(0);
            }
        }

        private void Generate(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public DirectedGraphMatrix Generate()
        {
            RandomNetworkGraphCreator generator = new RandomNetworkGraphCreator();

            generator.RowCount = int.Parse(RowCount.Text);
            generator.NodePropability = double.Parse(NodePropability.Text) / 100.0;

            return generator.Generate();
        }

        public DirectedGraphMatrix GenerateSkojarzenie()
        {
            RandomNetworkGraphCreator generator = new RandomNetworkGraphCreator();

            generator.MinimumNodePerRowLeft = int.Parse(NodeCountLeft.Text);
            generator.MinimumNodePerRowRight = int.Parse(NodeCountRight.Text);

            generator.NoRandoms = true;
            generator.RowCount = 2;

            var graph = generator.Generate();

            for(int x = 0; x < graph.NodesNr; ++x)
                for(int y = 0;y < graph.NodesNr; ++y)
                {
                    if (graph.GetConnection(x, y))
                        graph.setWeight(x, y, 1);
                }

            MaxFlow1 mf = new MaxFlow1(graph);
            mf.findMaxFlow(graph);

            var current = mf.getFlowMatrix();

            for (int y = 0; y < graph.NodesNr; ++y)
                for (int x = 0; x < graph.NodesNr; ++x)
                {
                    graph.Current[x, y] = current[x, y];
                }

            //teraz pokolorujmy
            int number = 1;

            for (int y = 0; y < graph.NodesNr; ++y)
                for (int x = 0; x < graph.NodesNr; ++x)
                {
                    if (y == 0 || y == graph.NodesNr - 1)
                        continue;
                    if (x == 0 || x == graph.NodesNr - 1)
                        continue;
                    if(graph.SkojarzenieKolor[x] == 0 && graph.GetCurrent(x, y) > 0)
                    {
                        graph.SkojarzenieKolor[x] = number;
                        graph.SkojarzenieKolor[y] = number++;
                    }
                }

            return graph;
        }

        private void OnClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DataContext = Generate();
        }
    }
}

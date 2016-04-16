using Graphs.Actions;
using Graphs.Data;
using Graphs.ViewModels;
using Graphs.Windows.Generators;
using Graphs.Windows.Project3;
using Graphs.Windows.Project4;
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

namespace Graphs
{
    /// <summary>
    /// Interaction logic for DirectedWindow.xaml
    /// </summary>
    public partial class DirectedWindow : Window
    {
        DirectedGraphMatrix Graph;
        DirectedWindowViewModel VM = new DirectedWindowViewModel();
        DirectionalGraphRenderer Renderer;

        public DirectedWindow()
        {
            InitializeComponent();

            Graph = GraphGenerator.CreateDirectional(GraphGenerator.generatorGER(10, 11));

            Renderer = new DirectionalGraphRenderer(Graph, GraphControl, VM);

            Graph.OnChange += onGraphChange;
            GraphControl.OnLineClick += createWeight;
            GraphControl.OnTwoNodeClickEvent += ConnectTwoNodes;
            Graph.OnChange();

            DataContext = VM;
        }

        private void createWeight(LineViewModel lineVM)
        {
            if (VM.ShowWeights)
            {
                var dialog = new SelectWeightWindow();
                dialog.Weight = Graph.getWeight(lineVM.StartNode, lineVM.EndNode);
                dialog.ShowDialog();
                int node1 = lineVM.StartNode;
                int node2 = lineVM.EndNode;
                int weight = dialog.Weight;

                Graph.setWeight(node1, node2, weight);
                Graph.OnChange();
            }
        }

        private void ConnectTwoNodes(int node1, int node2)
        {
            if (Graph.GetConnection(node1, node2) == false)
            {
                Graph.MakeConnection(node1, node2);
                Graph.OnChange();
            }
            else
            {
                Graph.RemoveConnection(node1, node2);
                Graph.OnChange();
            }
        }


        private void onGraphChange()
        {
            
        }

        private void GraphControlResize(object sender, SizeChangedEventArgs e)
        {
            Graph.OnChange();
        }

        private void ListChanged(object sender, RoutedEventArgs args)
        {

        }

        private void GenerateGraph(object sender, RoutedEventArgs args)
        {
            if (sender == ErdosRenyiMenuItem)
            {
                var w = new ErdosGenerator();
                try
                {
                    w.ShowDialog();
                    Graph.Clear();
                    Graph.Set(GraphGenerator.CreateDirectional(w.DataContext as GraphMatrix));
                    Graph.OnChange();
                }
                catch (Exception e)
                {
                    MessageBoxResult result = MessageBox.Show("Coś poszło nie tak"
                        + System.Environment.NewLine
                        + e.Message
                        );
                }
            }
            else if (sender == SecondGeneratorMenuItem)
            {
                var w = new SecondGenerator();
                try
                {

                    w.ShowDialog();
                    Graph.Clear();
                    Graph.Set(GraphGenerator.CreateDirectional(w.DataContext as GraphMatrix));
                    Graph.OnChange();
                }
                catch (Exception e)
                {
                    MessageBoxResult result = MessageBox.Show("Coś poszło nie tak"
                        + System.Environment.NewLine
                        + e.Message
                        );
                }
            }

            Graph.Set(GraphGenerator.CreateRandomDirectedWeights(Graph));
            Graph.OnChange();

        }

        private void OnRederTurnOn(object sender, RoutedEventArgs e)
        {
            Renderer.Render();
        }

        private void OnRederTurnOff(object sender, RoutedEventArgs e)
        {
            GraphControl.VM = new GraphViewModel();
        }

        private void BellmanFord(object sender, RoutedEventArgs e)
        {
            var dialog = new Dijkstra();

            dialog.ShowDialog();

            int startNode = dialog.StartNode - 1;
            int endNode = dialog.EndNode - 1;

            List<int> path = Directed.BellmanFord(Graph, startNode, endNode);
            
            string message = "Znaleziona sciezka : " + Environment.NewLine;
            if(path != null)
                path.ForEach(n => message += (n + 1) + " ");
            else
                message = "Nie znaleziono sciezki";

            MessageBox.Show(message);
        }

        private void CreateAcyclic(object sender, RoutedEventArgs arg)
        {
            var w = new CreateAcycligGraphWindow();
            try
            {
                w.ShowDialog();
                Graph.Clear();
                Graph.Set(w.Generate());
                Graph.OnChange();
            }
            catch (Exception e)
            {
                MessageBoxResult result = MessageBox.Show("Coś poszło nie tak"
                    + System.Environment.NewLine
                    + e.Message
                    );
            }

            Renderer.Displayer = new DirectedColumnDisplayer();
        }

        private void Dijkstra(object sender, RoutedEventArgs e)
        {
            var dialog = new Dijkstra();

            dialog.ShowDialog();

            int startNode = dialog.StartNode - 1;
            int endNode = dialog.EndNode - 1;

            List<int> path = PathFinding.Dijkstra(Graph, startNode, endNode);

            string message = "Znaleziona sciezka : " + Environment.NewLine;
            if (path != null)
                path.ForEach(n => message += (n + 1) + " ");
            else
                message = "Nie znaleziono sciezki";

            MessageBox.Show(message);
        }

        private void TopologicSorting(object sender, RoutedEventArgs e)
        {
            var sorted = Directed.TopologicSorting(Graph);

            string message = "";
            foreach (var node in sorted)
                message += (node + 1) + Environment.NewLine;

            MessageBox.Show(message);
        }
    }
}

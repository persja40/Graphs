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

        private void Johnson(object sender, RoutedEventArgs e)
        {
            var john = Directed.Johnson(Graph);

            var message = "";

            for (int y = 0; y < Graph.NodesNr; ++y)
            {
                for (int x = 0; x < Graph.NodesNr; ++x)
                {
                    if (john[y, x] < 20000000)
                    {
                        message += john[y, x];
                        if (john[y, x] < 10)
                            message += " ";
                        if (john[y, x] < 100)
                            message += " ";
                        message += " ";
                    }
                    else
                    {
                        message += " -  ";
                    }
                }
                message += Environment.NewLine;
            }

            MessageBox.Show(message);
        }

        private void Floyd(object sender, RoutedEventArgs e)
        {
            var john = Directed.FloydWarshall(Graph);

            var message = "";

            for (int y = 0; y < Graph.NodesNr; ++y)
            {
                for (int x = 0; x < Graph.NodesNr; ++x)
                {
                    if (john[y, x] < 20000000)
                    {
                        message += john[y, x];
                        if (john[y, x] < 10)
                            message += " ";
                        if (john[y, x] < 100)
                            message += " ";
                        message += " ";
                    }
                    else
                    {
                        message += " -  ";
                    }
                }
                message += Environment.NewLine;
            }

            MessageBox.Show(message);
        }

        private void ShowSilne(object sender, RoutedEventArgs e)
        {
            var cycles = Directed.spojne(Graph);

            var message = "Znalazłem takie silne spojne skladowe : ";
            foreach (var cycle in cycles)
            {
                message += Environment.NewLine;
                foreach (var node in cycle)
                {
                    message += (node + 1) + " ";
                }
            }

            MessageBox.Show(message);
        }

        private void ShowCycles(object sender, RoutedEventArgs e)
        {
            var cycles = Directed.circuts(Graph);

            var message = "Znalazłem takie cykle : ";
            foreach(var cycle in cycles)
            {
                message += Environment.NewLine;
                foreach (var node in cycle)
                {
                    message += (node+1) + " ";
                }
            }

            MessageBox.Show(message);
        }

        private void UjemneCykle(object sender, RoutedEventArgs e)
        {
            var test = Directed.ujemnyCykl(Graph, Graph.weights);

            MessageBox.Show(test ? "Jest ujemny cykl" : "Nie ma ujemnego cyklu");
        }

        private void MaxSpojna(object sender, RoutedEventArgs e)
        {
            Graph.Set(Directed.Directedmaxspojny(Graph));
            Graph.OnChange();
        }

        private void BFDistance(object sender, RoutedEventArgs e)
        { 

            var message = "";

            for (int y = 0; y < Graph.NodesNr; ++y)
            {
                for (int x = 0; x < Graph.NodesNr; ++x)
                {
                    var distList = Directed.BellmanFord(Graph, x, y);
                    if(distList == null)
                    {
                        message += " -  ";
                        continue;
                    }
                    var dist = 0;
                    for(int i = 1; i < distList.Count;++i)
                    {
                        dist += Graph.getWeight(distList[i - 1], distList[i]);
                    }
                    if (dist < 20000000)
                    {
                        message += dist;
                        if (dist < 10)
                            message += " ";
                        if (dist < 100)
                            message += " ";
                        message += " ";
                    }
                    else
                    {
                        message += " -  ";
                    }
                }
                message += Environment.NewLine;
            }

            MessageBox.Show(message);
        }
    }
}

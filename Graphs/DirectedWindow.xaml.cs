using Graphs.Actions;
using Graphs.Data;
using Graphs.ViewModels;
using Graphs.Windows.Generators;
using Graphs.Windows.Project3;
using Graphs.Windows.Project4;
using Graphs.Windows.Project5;
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
            if(VM.RegenerateMatrix)
            prepareMatrix();
            if(VM.RegenerateList)
            prepareGraphList();
            if(VM.RegenerateMatrixInc)
            prepareMatrixInc();
        }
        private void prepareMatrixInc()
        {
            MatrixIncViewModel vm = new MatrixIncViewModel(Graph.NodesNr, Graph.ConnectionCount);

            var matrixInc = Converter.ConvertToSMatrixInc(Converter.ConvertToSList(Graph));
            for (int connection = 0; connection < matrixInc.ConnectNr; ++connection)
                for (int node = 0; node < matrixInc.NodesNr; ++node)
                {
                    vm.Connections[node, connection] = matrixInc.GetConnectionArray(node, connection) ? 1 : 0;
                }

            MatrixIncControl.DataContext = vm;
        }

        private void prepareMatrix()
        {
            MatrixViewModel vm = new MatrixViewModel(Graph.NodesNr);

            for (int y = vm.NodeCount - 1; y >= 0; --y)
                for (int x = 0; x < vm.NodeCount; ++x)
                {
                    vm.Connections[x, y] = Graph.GetConnection(x, y) ? Graph.getWeight(x, y) : 0;
                }

            MatrixControl.DataContext = vm;
        }

        private void prepareGraphList()
        {
            var vm = GraphListControl.DataContext as GraphListViewModel;
            vm.Items.Clear();
            var graphList = Converter.ConvertToSList(Graph);

            for (int i = 0; i < graphList.NodesNr; ++i)
            {
                var connections = new ObservableCollection<int>(graphList.GetConnections(i));
                var nodeVM = new GraphListItemViewModel()
                {
                    ConnectedNodes = connections,
                    NodeNumber = i
                };
                vm.Items.Add(nodeVM);
            }
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
            Renderer.Displayer = new DirectedCircleDisplayer();

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

        private void BellmanFordStart(object sender, RoutedEventArgs e)
        {
            var dialog = new Dijkstra(true);


            dialog.ShowDialog();

            int startNode = dialog.StartNode - 1;

            string message = "";

            for(int i = 0;i < Graph.NodesNr;++i)
            {
                if (i == startNode)
                    continue;
                List<int> path = Directed.BellmanFord(Graph, startNode, i);

                message += string.Format("Node {0} : ", i + 1);
                if (path != null)
                {
                    path.ForEach(n => message += (n + 1) + " ");
                }
                else
                {
                    message += "brak sciezki";
                }

                message += Environment.NewLine;
            }

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
            var message = "";
            try
            {
                var john = Directed.Johnson(Graph);



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
            }
            catch(Exception)
            {
                message = "Wykryto ujemny cykl (Sprawdzony za pomoca zewnetrznej metody)";
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
                //for (int i = 1; i < cycle.Count; ++i)
                //{
                //    if(Graph.GetConnection(cycle[i-1], cycle))
                //}
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
            Renderer.Displayer = new DirectedCircleDisplayer();
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

        private void GenerateFlowNetwork(object sender, RoutedEventArgs args)
        {
            var w = new CreateFlowNetworkWindow();
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

        private void MaxFlow(object sender, RoutedEventArgs args)
        {
            MaxFlow1 mf = new MaxFlow1(Graph);
            string message = string.Format("Maxymalny przeplyw to {0}", mf.findMaxFlow(Graph));

            var current = mf.getFlowMatrix();

            for(int y = 0; y < Graph.NodesNr; ++y)
                for(int x = 0;x < Graph.NodesNr; ++x)
                {
                    Graph.Current[x, y] = current[y, x];
                }

            Renderer.Displayer = new DirectedColumnFlowDisplayer();
            MessageBox.Show(message);
        }

        private void ResetWeight(object sender, RoutedEventArgs e)
        {
            for (int y = 0; y < Graph.NodesNr; ++y)
                for (int x = 0; x < Graph.NodesNr; ++x)
                {
                    if(Graph.getWeight(x,y) < 1000000)
                    Graph.setWeight(x, y, 1);
                }

            Renderer.Displayer = Renderer.Displayer;
        }
    }
}

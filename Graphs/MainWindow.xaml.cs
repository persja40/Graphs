using Graphs.Actions;
using Graphs.Data;
using Graphs.Helpers;
using Graphs.TestWindows;
using Graphs.ViewModels;
using Graphs.Windows.Generators;
using Graphs.Windows.Project2;
using Graphs.Windows.Project3;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Graphs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GraphMatrix Graph { get; set; }
        public GraphRenderer GraphRenderer { get; set; }
        public MainWindowViewModel VM { get; set; } = new MainWindowViewModel();

        public static MainWindow Instance;


        public MainWindow()
        {
            if (Instance == null)
                Instance = this;

            InitializeComponent();
            Project4(null, null);

            Graph = GraphGenerator.generatorRegular(2);

            GraphRenderer = new GraphRenderer(Graph, GraphControl, VM);

            GraphListControl.DataContext = new GraphListViewModel();

            GraphControl.OnTwoNodeClickEvent += ConnectTwoNodes;
            GraphControl.OnLineClick += createWeight;


            Graph.OnChange += onGraphChange;
            onGraphChange();

            this.DataContext = VM;
        }

        private void createWeight(LineViewModel lineVM)
        {
           if(VM.ShowWeights)
            {
                var dialog = new SelectWeightWindow();
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
            if (VM.RegenerateList)
                prepareGraphList();
            if (VM.RegenerateMatrix)
                prepareMatrix();
            if (VM.RegenerateMatrixInc)
                prepareMatrixInc();
        }

        private void prepareMatrixInc()
        {
            MatrixIncViewModel vm = new MatrixIncViewModel(Graph.NodesNr, Graph.ConnectionCount);

            var matrixInc = Converter.ConvertToMatrixInc(Graph);
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
            var graphList = Converter.ConvertToList(Graph);

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

        private void openWindow<T>()
            where T : Window, new()
        {
            if (!WindowHelper.IsWindowOpen<T>())
            {
                var window = new T();
                window.Show();
            }
        }


        private void ShowAuthors(object sender, RoutedEventArgs e)
        {
            openWindow<Authors>();

        }

        private void OpenGraphListItemTest(object sender, RoutedEventArgs e)
        {
            openWindow<GraphListItemTest>();
        }

        private void OpenGraphListTest(object sender, RoutedEventArgs e)
        {
            openWindow<GraphListTest>();
        }

        private void GraphControlResize(object sender, SizeChangedEventArgs e)
        {
            GraphRenderer.Render();
        }

        private void GenerateGraph(object sender, RoutedEventArgs args)
        {
            Stopwatch watch = null;
            //long before = 0;
            long after = 0;
            if (sender == ErdosRenyiMenuItem)
            {
                var w = new ErdosGenerator();
                try
                {
                    w.ShowDialog();
                    watch = Stopwatch.StartNew();
                    Graph.Clear();
                    Graph.Set(w.DataContext as GraphMatrix);
                    //before = watch.ElapsedMilliseconds;
                    Graph.OnChange();
                    watch.Stop();
                    after = watch.ElapsedMilliseconds;
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
                    watch = Stopwatch.StartNew();
                    Graph.Clear();
                    Graph.Set(w.DataContext as GraphMatrix);
                    //before = watch.ElapsedMilliseconds;
                    Graph.OnChange();
                    watch.Stop();
                    after = watch.ElapsedMilliseconds;
                }
                catch (Exception e)
                {
                    MessageBoxResult result = MessageBox.Show("Coś poszło nie tak"
                        + System.Environment.NewLine
                        + e.Message
                        );
                }
            }

            if (watch != null)
            {


                //MessageBox.Show(
                //    "Before = " + (double)(before / 1000.0) +
                //    System.Environment.NewLine +
                //    "After = " + (double)(after / 1000.0));
            }

        }

        private void ListChanged(object sender, RoutedEventArgs args)
        {
            var vm = GraphListControl.DataContext as GraphListViewModel;
            var matrix = Converter.ConvertToMatrix(vm.GetList());
            Graph.Set(matrix);
            Graph.OnChange();
        }

        private void GenerateRandomConnection(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 1000; ++i)
            {
                Random rand = new Random();
                int node1 = rand.Next(0, Graph.NodesNr);
                int node2 = rand.Next(0, Graph.NodesNr);
                if (node1 != node2)
                {
                    Graph.MakeConnection(node1, node2);
                    Graph.OnChange();
                    break;
                }

            }

        }

        private void CreateCoherentGraph(object sender, RoutedEventArgs e)
        {
            Graph.Set(Actions.Misc.CreateBiggestCoherent(Graph));
            Graph.OnChange();
        }

        private void CreateNew(object sender, RoutedEventArgs e)
        {
            GraphMatrix newGraph = new GraphMatrix(1);
            Graph.Set(newGraph);
            Graph.OnChange();
        }

        private void SaveGraph(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "Graph";
            dlg.DefaultExt = ".matrix";
            dlg.Filter = "Matrix|*.matrix|List|*.list|Incidency|*.inc";
            dlg.InitialDirectory = SaveLoadWindowHelper.LoadCurrentDialogDirectory();

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                if (dlg.FileName.ToLower().EndsWith("matrix"))
                    GraphLoad.SaveMatrix(Graph, dlg.FileName);

                else if (dlg.FileName.ToLower().EndsWith("list"))
                    GraphLoad.SaveList(Converter.ConvertToList(Graph), dlg.FileName);

                else if (dlg.FileName.ToLower().EndsWith("inc"))
                    GraphLoad.SaveMatrixInc(Converter.ConvertToMatrixInc(Graph), dlg.FileName);
                SaveLoadWindowHelper.SaveCurrentDialogDirectory(System.IO.Path.GetDirectoryName(dlg.FileName));
            }
            else
            {
                SaveLoadWindowHelper.SaveCurrentDialogDirectory(dlg.InitialDirectory);
            }
        }

        private void LoadGraph(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".matrix";
            dlg.Filter = "Matrix|*.matrix|List|*.list|Incidency|*.inc";
            dlg.InitialDirectory = SaveLoadWindowHelper.LoadCurrentDialogDirectory();

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                if (dlg.FileName.ToLower().EndsWith("matrix"))
                    Graph.Set(GraphLoad.LoadMatrix(dlg.FileName));

                else if (dlg.FileName.ToLower().EndsWith("list"))
                    Graph.Set(
                        Converter.ConvertToMatrix(GraphLoad.LoadList(dlg.FileName))
                        );

                else if (dlg.FileName.ToLower().EndsWith("inc"))
                    Graph.Set(
                        Converter.ConvertToMatrix(GraphLoad.LoadMatrixInc(dlg.FileName))
                        );

                SaveLoadWindowHelper.SaveCurrentDialogDirectory(System.IO.Path.GetDirectoryName(dlg.FileName));
            }
            else
            {
                SaveLoadWindowHelper.SaveCurrentDialogDirectory(dlg.InitialDirectory);
            }
            Graph.OnChange();
        }

        private void ExitApplication(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void IsHamilton(object sender, RoutedEventArgs e)
        {
            bool value = Hamilton.IsHamilton(Graph);

            string message = "Graf " + (value ? "" : "nie") + " jest Hamiltonowski";

            MessageBox.Show(message);
        }

        private void IsGraphical(object sender, RoutedEventArgs e)
        {
            var dialog = new Windows.Project2.IsGraphical();

            dialog.ShowDialog();

            bool result = Actions.Misc.Exists(dialog.Degrees.ToList());

            string message = string.Format("This list od node degrees is {0} graphical", result ? "" : "not");

            MessageBox.Show(message);
        }

        private void OnRederTurnOn(object sender, RoutedEventArgs e)
        {
            GraphRenderer.Render();
        }

        private void OnRederTurnOff(object sender, RoutedEventArgs e)
        {
            GraphControl.VM = new GraphViewModel();
        }

        private void Randomize(object sender, RoutedEventArgs e)
        {
            Graph.Set(GraphGenerator.Randomize(Graph));
            Graph.OnChange();
        }

        private void CreateEuler(object sender, RoutedEventArgs e)
        {
            var window = new CreateEulerWindow();
            window.ShowDialog();
            int nodes = window.NodesCount;

            Graph.Set(EulerGraph.RandEulerGraph(nodes));
            Graph.OnChange();

        }

        private void IsEuler(object sender, RoutedEventArgs e)
        {
            List<int> path = EulerGraph.EulerianPath(Graph);
            string message = "Oto trasa eulera : ";

            

            if(path != null)
            {
                path.ForEach(v => message += (v + 1) + " ");
                message = "Graf ma cykl eulera." + Environment.NewLine + message;
                MessageBox.Show(message);
            }
            else
            {
                message = "Graf nie ma cyklu eulera";
                MessageBox.Show(message);
            }

        }

        private void GenerateKRegular(object sender, RoutedEventArgs a)
        {
            var window = new CreateRegularWindow();
            window.ShowDialog();
            int nodes = window.NodesCount;
            int nodeDegree = window.NodeDegree;

            Graph.Set(GraphGenerator.generatorRegular(nodeDegree, nodes));
            Graph.OnChange();
        }

        private void CreateRandomWeights(object sender, RoutedEventArgs e)
        {
            Graph.Set(GraphGenerator.CreateRandomWeights(Graph, 1, 10));
            Graph.OnChange();
        }
        private void CalculateDijkstra(object sender, RoutedEventArgs e)
        {
            var dialog = new Dijkstra();

            dialog.ShowDialog();

            int startNode = dialog.StartNode - 1;
            int endNode = dialog.EndNode - 1;

            List<int> path = PathFinding.Dijkstra(Graph, startNode, endNode);

            string message = "Znaleziona sciezka : " + Environment.NewLine;

            path.ForEach(n => message += (n + 1) + " ");

            MessageBox.Show(message);

        }

        private void GraphMinMaxCentre(object sender, RoutedEventArgs e)
        {
            int centreID = Actions.Misc.graphCentrumMinimax(Graph) + 1;

            string message = "MinMax centrum of the graph is : " + centreID;
             
            MessageBox.Show(message);

        }

        private void GraphCentre(object sender, RoutedEventArgs e)
        {
            int centreID = Actions.Misc.graphCentrum(Graph) + 1;

            string message = "MinMax centrum of the graph is : " + centreID;

            MessageBox.Show(message);
        }

        private void DistanceMatrix(object sender, RoutedEventArgs e)
        {
            var weightDistance = Actions.Misc.distancesMatrix(Graph);

            string message = "";

            for(int i = 0;i < Graph.NodesNr; ++i)
            {
                message += "Wagi dla " + (i + 1) + Environment.NewLine;
                for(int j = 0; j < Graph.NodesNr; ++j)
                {
                    var weight = weightDistance[i, j];
                    message += string.Format(" -> {0} : {1}{2}", j + 1, weight, Environment.NewLine);
                }
            }

            MessageBox.Show(message);


        }

        private void Project4(object sender, RoutedEventArgs e)
        {
            DirectedWindow wind = new DirectedWindow();

            wind.ShowDialog();
        }

        private void SpanningTree(object sender, RoutedEventArgs e)
        {
            Graph.Set(GraphGenerator.Prim(Graph));
            Graph.OnChange();
            
        }
    }
}

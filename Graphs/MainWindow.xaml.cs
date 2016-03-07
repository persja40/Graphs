using Graphs.Actions;
using Graphs.Data;
using Graphs.TestWindows;
using Graphs.ViewModels;
using Graphs.Windows.Generators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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


        public MainWindow()
        {
            InitializeComponent();

            Graph = new GraphMatrix(5);

            GraphRenderer = new GraphRenderer(new GraphMatrix(1), GraphControl);

            GraphListControl.DataContext = new GraphListViewModel();

            

            Graph.OnChange += onGraphChange;
            onGraphChange();
        }

        private void onGraphChange()
        {
            prepareGraphList();
            //prepareMatrix();
            //prepareMatrixInc();
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

            for (int y = vm.NodeCount - 1; y  >= 0; --y)
                for (int x = 0; x < vm.NodeCount; ++x)
                {
                    vm.Connections[x, y] = Graph.GetConnection(x, y) ? 1 : 0;
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
            if (!Helpers.IsWindowOpen<T>())
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
            long before = 0, after = 0;
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
                catch(Exception e)
                {
                    MessageBoxResult result = MessageBox.Show("Coś poszło nie tak"
                        + System.Environment.NewLine
                        + e.Message
                        );
                }
            }
            else if(sender == SecondGeneratorMenuItem)
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
            Random rand = new Random();
            int node1 = rand.Next(0, Graph.NodesNr);
            int node2 = rand.Next(0, Graph.NodesNr);
            if (node1 != node2)
                Graph.MakeConnection(node1, node2);
            Graph.OnChange();
        }
    }
}

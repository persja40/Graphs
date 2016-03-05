using Graphs.Actions;
using Graphs.Data;
using Graphs.TestWindows;
using Graphs.ViewModels;
using Graphs.Windows.Generators;
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
            GraphRenderer = new GraphRenderer(Graph, GraphControl);

            GraphListControl.DataContext = new GraphListViewModel();

            Graph.OnChange += onGraphChange;
        }

        private void onGraphChange()
        {
            var vm = GraphListControl.DataContext as GraphListViewModel;
            vm.Items.Clear();
            var graphList = Converter.ConvertToList(Graph);

            for(int i = 0; i < graphList.NodesNr; ++i)
            {
                var connections = new ObservableCollection<int>(graphList.GetConnections(i));
                var nodeVM = new GraphListItemViewModel()
                {
                    ConnectedNodes = connections,
                    NodeNumer = i
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

        private void GenerateGraph(object sender, RoutedEventArgs e)
        {
            if(sender == ErdosRenyiMenuItem)
            {
                var w = new ErdosGenerator();
                try
                {
                    w.ShowDialog();
                    Graph = w.DataContext as GraphMatrix;
                    GraphRenderer.Graph = Graph;
                }
                catch(Exception)
                {
                    MessageBoxResult result = MessageBox.Show("Coś poszło nie tak");
                }


               
            }

        }

        private void GenerateRandomConnection(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            int node1 = rand.Next(0, 5);
            int node2 = rand.Next(0, 5);
            if (node1 != node2)
                Graph.MakeConnection(node1, node2);
        }
    }
}

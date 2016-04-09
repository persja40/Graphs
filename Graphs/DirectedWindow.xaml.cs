using Graphs.Actions;
using Graphs.Data;
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

            Graph = GraphGenerator.CreateDirectional(GraphGenerator.generatorGER(5, 5));

            Renderer = new DirectionalGraphRenderer(Graph, GraphControl, VM);
            Renderer.Render();

            
        }

        private void GraphControlResize(object sender, SizeChangedEventArgs e)
        {

        }

        private void ListChanged(object sender, RoutedEventArgs args)
        {

        }
    }
}

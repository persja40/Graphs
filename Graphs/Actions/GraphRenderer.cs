using Graphs.Data;
using Graphs.UserControls;
using Graphs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Graphs.Actions
{
    public class GraphRenderer
    {

        GraphMatrix _graph;
        public GraphMatrix Graph
        {
            get
            {
                return _graph;
            }
            set
            {
                if (_graph != null)
                    _graph.OnChange -= onGraphChange;
                _graph = value;
                _graph.OnChange += onGraphChange;
            }
        }
        public GraphControl GraphControl { get; set; }

        private MainWindowViewModel mainWindowVM = null;

        public GraphRenderer(GraphMatrix Graph, GraphControl GraphControl, MainWindowViewModel vm)
        {
            this.GraphControl = GraphControl;
            this.Graph = Graph;
            this.mainWindowVM = vm;
        }

        private void onGraphChange()
        {
            Render();
        }

        public void Render()
        {
            if (this.mainWindowVM.RegenerateGraphView == false)
                return;
            GraphViewModel vm = new GraphViewModel();
            double r = Math.Sqrt(Math.Pow(GraphControl.ActualHeight, 1.8) + Math.Pow(GraphControl.ActualWidth, 1.8)) / 20;


            for (int i = 0; i < Graph.NodesNr; ++i)
            {
                double arc = 2 * Math.PI / Graph.NodesNr * i;
                double x = GraphControl.ActualWidth / 2 + (GraphControl.ActualWidth / 2 - r ) * Math.Cos(arc);
                double y = GraphControl.ActualHeight / 2 + (GraphControl.ActualHeight / 2 - r ) * Math.Sin(arc);

                vm.Nodes.Add(new CircleViewModel()
                {
                    X = x,
                    Y = y,
                    Radius = r,
                    Color = new SolidColorBrush(Colors.Yellow),
                    Number = i + 1,
                    NodeNumber = i
                });
            }


            for (int y = 0; y < Graph.NodesNr; ++y)
                for (int x = 0; x < Graph.NodesNr; ++x)
                {
                    if (Graph.GetConnection(x, y) == false)
                        continue;
                    double arc1 = 2 * Math.PI / Graph.NodesNr * y;
                    double arc2 = 2 * Math.PI / Graph.NodesNr * x;

                    double x1 = GraphControl.ActualWidth / 2 + (GraphControl.ActualWidth / 2 - r ) * Math.Cos(arc1);
                    double y1 = GraphControl.ActualHeight / 2 + (GraphControl.ActualHeight / 2 - r) * Math.Sin(arc1);

                    double x2 = GraphControl.ActualWidth / 2 + (GraphControl.ActualWidth / 2 - r) * Math.Cos(arc2);
                    double y2 = GraphControl.ActualHeight / 2 + (GraphControl.ActualHeight / 2 - r) * Math.Sin(arc2);

                    int weight = Graph.getWeight(x, y);

                    byte redBrightness = 0;
                    if(mainWindowVM.ShowWeights)
                    {
                        redBrightness = (byte)((double)weight / (double)Graph.MaxWeight * 255.0);
                    }

                    LineViewModel lineVM = new LineViewModel()
                    {
                        X1 = x1,
                        Y1 = y1,
                        X2 = x2,
                        Y2 = y2,
                        StartNode = x,
                        EndNode = y,
                        Color = Color.FromRgb(redBrightness, 0, 0)
                    };
                    vm.Connections.Add(lineVM);

                    if (mainWindowVM.ShowWeights)
                    {
                        LineViewModel hintVM = new LineViewModel(lineVM)
                        {
                            Hint = string.Format("Weight : {0}", weight),
                            Color = Colors.Transparent,
                            Thickness = 8
                        };

                        vm.Connections.Add(hintVM);
                    }

                    
                }

            GraphControl.VM = vm;
        }

    }
}

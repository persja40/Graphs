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
    public class DirectionalGraphRenderer
    {
        DirectedGraphMatrix _graph;
        public DirectedGraphMatrix Graph
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

        private DirectedWindowViewModel DirectedWindowVM = null;

        public DirectionalGraphRenderer(DirectedGraphMatrix Graph, GraphControl GraphControl, DirectedWindowViewModel vm)
        {
            this.GraphControl = GraphControl;
            this.Graph = Graph;
            this.DirectedWindowVM = vm;
        }
        
        private void onGraphChange()
        {
            Render();
        }
        
        public void Render()
        {
            if (this.DirectedWindowVM.RegenerateGraphView == false)
                return;
            DirectedGraphViewModel vm = new DirectedGraphViewModel();
            double r = Math.Sqrt(Math.Pow(GraphControl.ActualHeight, 1.8) + Math.Pow(GraphControl.ActualWidth, 1.8)) / 20;


            for (int i = 0; i < Graph.NodesNr; ++i)
            {
                double arc = 2 * Math.PI / Graph.NodesNr * i;
                double x = GraphControl.ActualWidth / 2 + (GraphControl.ActualWidth / 2 - r) * Math.Cos(arc);
                double y = GraphControl.ActualHeight / 2 + (GraphControl.ActualHeight / 2 - r) * Math.Sin(arc);

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
                    if (Graph.GetConnection(y, x) == false)
                        continue;
                    double arc1 = 2 * Math.PI / Graph.NodesNr * y;
                    double arc2 = 2 * Math.PI / Graph.NodesNr * x;

                    double x1 = GraphControl.ActualWidth / 2 + (GraphControl.ActualWidth / 2 - r) * Math.Cos(arc1);
                    double y1 = GraphControl.ActualHeight / 2 + (GraphControl.ActualHeight / 2 - r) * Math.Sin(arc1);

                    double x2 = GraphControl.ActualWidth / 2 + (GraphControl.ActualWidth / 2 - r) * Math.Cos(arc2);
                    double y2 = GraphControl.ActualHeight / 2 + (GraphControl.ActualHeight / 2 - r) * Math.Sin(arc2);

                    int weight = Graph.getWeight(y, x);

                    byte redBrightness = 0;
                    if (DirectedWindowVM.ShowWeights)
                    {
                        redBrightness = (byte)((Math.Abs(Graph.MaxWeight - Graph.MinWeight) - Math.Abs(Graph.MaxWeight - weight) ) / (double)(Math.Abs(Graph.MaxWeight - Graph.MinWeight)) * 255.0);
                    }

                    LineViewModel lineVM = new LineViewModel()
                    {
                        X1 = x1,
                        Y1 = y1,
                        X2 = x2,
                        Y2 = y2,
                        StartNode = y,
                        EndNode = x,
                        Color = Color.FromRgb(redBrightness, 0, 0)
                    };
                    vm.Connections.Add(lineVM);
                    double a = (y2 - y1) / (x2 - x1);
                    double b = y2 / (a * x2);
                    double length = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
                    double newLength = length - r / 2;

                    double ratio = newLength / length;
                    double newX = x1 + (x2 - x1) * ratio;
                    double newY = y1 + (y2 - y1) * ratio;

                    TriangleViewModel triangleVm = new TriangleViewModel()
                    {
                        X = newX - 10,
                        Y = newY - 5,
                        Angle = lineVM.Angle * (180.0 / Math.PI) + 90.0
                    };

                    vm.Triangles.Add(triangleVm);

                    if (DirectedWindowVM.ShowWeights)
                    {
                        LineViewModel hintVM = new LineViewModel(lineVM)
                        {
                            Hint = string.Format("Weight : {0}, {1} , {2}", weight, y, x),
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

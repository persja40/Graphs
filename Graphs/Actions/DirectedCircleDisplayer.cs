using Graphs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Graphs.Actions
{
    class DirectedCircleDisplayer : IDirectedDisplayer
    {
        public void Display(DirectionalGraphRenderer renderer)
        {
            if (renderer.DirectedWindowVM.RegenerateGraphView == false)
                return;
            DirectedGraphViewModel vm = new DirectedGraphViewModel();
            double r = Math.Sqrt(Math.Pow(renderer.GraphControl.ActualHeight, 1.8) + Math.Pow(renderer.GraphControl.ActualWidth, 1.8)) / 20;


            for (int i = 0; i < renderer.Graph.NodesNr; ++i)
            {
                double arc = 2 * Math.PI / renderer.Graph.NodesNr * i;
                double x = renderer.GraphControl.ActualWidth / 2 + (renderer.GraphControl.ActualWidth / 2 - r) * Math.Cos(arc);
                double y = renderer.GraphControl.ActualHeight / 2 + (renderer.GraphControl.ActualHeight / 2 - r) * Math.Sin(arc);

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


            for (int y = 0; y < renderer.Graph.NodesNr; ++y)
                for (int x = 0; x < renderer.Graph.NodesNr; ++x)
                {
                    if (renderer.Graph.GetConnection(y, x) == false)
                        continue;
                    double arc1 = 2 * Math.PI / renderer.Graph.NodesNr * y;
                    double arc2 = 2 * Math.PI / renderer.Graph.NodesNr * x;

                    double x1 = renderer.GraphControl.ActualWidth / 2 + (renderer.GraphControl.ActualWidth / 2 - r) * Math.Cos(arc1);
                    double y1 = renderer.GraphControl.ActualHeight / 2 + (renderer.GraphControl.ActualHeight / 2 - r) * Math.Sin(arc1);

                    double x2 = renderer.GraphControl.ActualWidth / 2 + (renderer.GraphControl.ActualWidth / 2 - r) * Math.Cos(arc2);
                    double y2 = renderer.GraphControl.ActualHeight / 2 + (renderer.GraphControl.ActualHeight / 2 - r) * Math.Sin(arc2);

                    int weight = renderer.Graph.getWeight(y, x);

                    byte redBrightness = 0;
                    if (renderer.DirectedWindowVM.ShowWeights)
                    {
                        redBrightness = (byte)((Math.Abs(renderer.Graph.MaxWeight - renderer.Graph.MinWeight) - Math.Abs(renderer.Graph.MaxWeight - weight)) / (double)(Math.Abs(renderer.Graph.MaxWeight - renderer.Graph.MinWeight)) * 255.0);
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

                    if (renderer.DirectedWindowVM.ShowWeights)
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

            renderer.GraphControl.VM = vm;
        }
    }
}

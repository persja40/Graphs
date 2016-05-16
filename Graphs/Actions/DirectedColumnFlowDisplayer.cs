using Graphs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Graphs.Actions
{
    class DirectedColumnFlowDisplayer : IDirectedDisplayer
    {
        public void Display(DirectionalGraphRenderer renderer)
        {
            if (renderer.DirectedWindowVM.RegenerateGraphView == false)
                return;
            DirectedGraphViewModel vm = new DirectedGraphViewModel();
            double r = Math.Sqrt(Math.Pow(renderer.GraphControl.ActualHeight, 1.8) + Math.Pow(renderer.GraphControl.ActualWidth, 1.8)) / 20;


            for (int i = 0; i < renderer.Graph.NodesNr; ++i)
            {
                double ratio = (double)(renderer.Graph.NodeOrderInColumn(i)) / (double)(renderer.Graph.NodesInColumn(i) - 1);
                if (double.IsNaN(ratio) || double.IsInfinity(ratio))
                    ratio = 0.0;

                double x = r + (renderer.GraphControl.ActualWidth - 2 * r) * (double)(renderer.Graph.Columns[i]) / (double)(renderer.Graph.ColumnsCount());
                double y = r + (renderer.GraphControl.ActualHeight - 2 * r) * ratio;

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

                    var ratio1 = (double)(renderer.Graph.NodeOrderInColumn(y)) / (double)(renderer.Graph.NodesInColumn(y) - 1);
                    var ratio2 = (double)(renderer.Graph.NodeOrderInColumn(x)) / (double)(renderer.Graph.NodesInColumn(x) - 1);
                    if (double.IsNaN(ratio1) || double.IsInfinity(ratio1))
                        ratio1 = 0.0;
                    if (double.IsNaN(ratio2) || double.IsInfinity(ratio2))
                        ratio2 = 0.0;

                    double x1 = r + (renderer.GraphControl.ActualWidth - 2 * r) * (double)(renderer.Graph.Columns[y]) / (double)(renderer.Graph.ColumnsCount());
                    double y1 = r + (renderer.GraphControl.ActualHeight - 2 * r) * ratio1;

                    double x2 = r + (renderer.GraphControl.ActualWidth - 2 * r) * (double)(renderer.Graph.Columns[x]) / (double)(renderer.Graph.ColumnsCount());
                    double y2 = r + (renderer.GraphControl.ActualHeight - 2 * r) * ratio2;

                    int maxFlow = 0;
                    foreach (var flow in renderer.Graph.weights)
                    {
                        if (flow > 2000000)
                            continue;
                        if (flow > maxFlow)
                            maxFlow = flow;
                    }

                    var abc = (float)renderer.Graph.getWeight(y, x);

                    float flowRatio = ((float)renderer.Graph.getWeight(y, x) / (float)maxFlow);
                    Color flowColor = Colors.Green;
                    if (flowRatio <= 1.1)
                        flowColor = Color.FromRgb((byte)0, (byte)0, (byte)(flowRatio * 255));

                    int thickness = 25;
                    if (flowRatio <= 1.1)
                        thickness = (int)abc * 2;

                    LineViewModel lineVM = new LineViewModel()
                    {
                        X1 = x1,
                        Y1 = y1,
                        X2 = x2,
                        Y2 = y2,
                        StartNode = y,
                        EndNode = x,
                        //Color = flowColor,
                        Thickness = thickness
                    };
                   // vm.Connections.Add(lineVM);


                    maxFlow = 0;
                    foreach (var flow in renderer.Graph.Current)
                    {
                        if (flow > maxFlow)
                            maxFlow = flow;
                    }

                    abc = (float)renderer.Graph.GetCurrent(x, y);

                    flowRatio = (abc / (float)maxFlow);
                    flowColor = Colors.Green;
                    if (flowRatio <= 1.1)
                        flowColor = Color.FromRgb((byte)0, (byte)0,  (byte)(155 +  flowRatio * 100));

                    thickness = 15;
                    if (flowRatio <= 1.1)
                        thickness = (int)abc * 2;

                    lineVM = new LineViewModel()
                    {
                        X1 = x1,
                        Y1 = y1,
                        X2 = x2,
                        Y2 = y2,
                        StartNode = y,
                        EndNode = x,
                        Color = flowColor,
                        Hint = string.Format("Flow {0}", abc),
                        Thickness = thickness
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
                }

            renderer.GraphControl.VM = vm;
        }
    }
}

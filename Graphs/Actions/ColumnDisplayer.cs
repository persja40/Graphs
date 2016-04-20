using Graphs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Graphs.Actions
{
    public class ColumnDisplayer : IDisplayer
    {
        public void Display(GraphRenderer R)
        {
            if (R.mainWindowVM.RegenerateGraphView == false)
                return;
            GraphViewModel vm = new GraphViewModel();
            double r = Math.Sqrt(Math.Pow(R.GraphControl.ActualHeight, 1.8) + Math.Pow(R.GraphControl.ActualWidth, 1.8)) / 20;


            for (int i = 0; i < R.Graph.NodesNr; ++i)
            {
                double ratio = (double)(R.Graph.NodeOrderInColumn(i)) / (double)(R.Graph.NodesInColumn(i) - 1);
                if (double.IsNaN(ratio) || double.IsInfinity(ratio))
                    ratio = 0.0;

                double x = r + (R.GraphControl.ActualWidth - 2 * r) * (double)(R.Graph.Columns[i]) / (double)(R.Graph.ColumnsCount());
                double y = r + (R.GraphControl.ActualHeight - 2 * r) * ratio;

                if(R.Graph.NodesInColumn(i) - 1 == 0)
                {
                    y = (R.GraphControl.ActualHeight - 2 * r) / 2;
                }

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


            for (int y = 0; y < R.Graph.NodesNr; ++y)
                for (int x = 0; x < R.Graph.NodesNr; ++x)
                {
                    if (R.Graph.GetConnection(y, x) == false)
                        continue;

                    var ratio1 = (double)(R.Graph.NodeOrderInColumn(y)) / (double)(R.Graph.NodesInColumn(y) - 1);
                    var ratio2 = (double)(R.Graph.NodeOrderInColumn(x)) / (double)(R.Graph.NodesInColumn(x) - 1);
                    if (double.IsNaN(ratio1) || double.IsInfinity(ratio1))
                        ratio1 = 0.0;
                    if (double.IsNaN(ratio2) || double.IsInfinity(ratio2))
                        ratio2 = 0.0;

                    double x1 = r + (R.GraphControl.ActualWidth - 2 * r) * (double)(R.Graph.Columns[y]) / (double)(R.Graph.ColumnsCount());
                    double y1 = r + (R.GraphControl.ActualHeight - 2 * r) * ratio1;

                    double x2 = r + (R.GraphControl.ActualWidth - 2 * r) * (double)(R.Graph.Columns[x]) / (double)(R.Graph.ColumnsCount());
                    double y2 = r + (R.GraphControl.ActualHeight - 2 * r) * ratio2;

                    if (R.Graph.NodesInColumn(y) - 1 == 0)
                    {
                        y1 = (R.GraphControl.ActualHeight - 2 * r) / 2;
                    }

                    if (R.Graph.NodesInColumn(x) - 1 == 0)
                    {
                        y2 = (R.GraphControl.ActualHeight - 2 * r) / 2;
                    }


                    int weight = R.Graph.getWeight(x, y);
                    byte redBrightness = 0;
                    if (R.mainWindowVM.ShowWeights)
                    {
                        redBrightness = (byte)((double)weight / (double)R.Graph.MaxWeight * 255.0);
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
                }

            R.GraphControl.VM = vm;
        }
    }
}

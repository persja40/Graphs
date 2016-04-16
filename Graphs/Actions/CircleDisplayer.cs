using Graphs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Graphs.Actions
{
    public class CircleDisplayer : IDisplayer
    {
        public void Display(GraphRenderer R)
        {
            if (R.mainWindowVM.RegenerateGraphView == false)
                return;
            GraphViewModel vm = new GraphViewModel();
            double r = Math.Sqrt(Math.Pow(R.GraphControl.ActualHeight, 1.8) + Math.Pow(R.GraphControl.ActualWidth, 1.8)) / 20;


            for (int i = 0; i < R.Graph.NodesNr; ++i)
            {
                double arc = 2 * Math.PI / R.Graph.NodesNr * i;
                double x = R.GraphControl.ActualWidth / 2 + (R.GraphControl.ActualWidth / 2 - r) * Math.Cos(arc);
                double y = R.GraphControl.ActualHeight / 2 + (R.GraphControl.ActualHeight / 2 - r) * Math.Sin(arc);

                

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
                    if (R.Graph.GetConnection(x, y) == false)
                        continue;
                    double arc1 = 2 * Math.PI / R.Graph.NodesNr * y;
                    double arc2 = 2 * Math.PI / R.Graph.NodesNr * x;

                    double x1 = R.GraphControl.ActualWidth / 2 + (R.GraphControl.ActualWidth / 2 - r) * Math.Cos(arc1);
                    double y1 = R.GraphControl.ActualHeight / 2 + (R.GraphControl.ActualHeight / 2 - r) * Math.Sin(arc1);

                    double x2 = R.GraphControl.ActualWidth / 2 + (R.GraphControl.ActualWidth / 2 - r) * Math.Cos(arc2);
                    double y2 = R.GraphControl.ActualHeight / 2 + (R.GraphControl.ActualHeight / 2 - r) * Math.Sin(arc2);

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
                        StartNode = x,
                        EndNode = y,
                        Color = Color.FromRgb(redBrightness, 0, 0)
                    };
                    vm.Connections.Add(lineVM);

                    if (R.mainWindowVM.ShowWeights)
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

            R.GraphControl.VM = vm;
        }
    }
}

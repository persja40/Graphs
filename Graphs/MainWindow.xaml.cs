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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Graphs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GraphModel model;
        public MainWindow()
        {
            InitializeComponent();

            model  = new GraphModel(10);
            Random rand = new Random();
            for (int i = 0; i < 25; ++i)
            {
                int node1 = rand.Next(0, 10);
                int node2 = rand.Next(0, 10);

                model.AddConnection(node1, node2);
            }

        }


        private void Do()
        {
           
            GraphViewModel vm = new GraphViewModel();
            double r = Math.Sqrt(Math.Pow(MyGraphControl.ActualHeight,1.8) + Math.Pow(MyGraphControl.ActualWidth, 1.8)) / 20;
            for (int i = 0; i < 10; ++i)
            {
                double arc = Math.PI / 5 * i;
                double x = MyGraphControl.ActualWidth / 2 + (MyGraphControl.ActualWidth / 2 - r / 2) * Math.Cos(arc);
                double y = MyGraphControl.ActualHeight / 2 + (MyGraphControl.ActualHeight / 2 - r / 2) * Math.Sin(arc);

                vm.Nodes.Add(new CircleViewModel() { X = x, Y = y, Radius = r, Color = Colors.Yellow });
            }

            for(int y = 0;y < 10;++y)
                for(int x = 0;x < 10;++x)
                {
                    if (model.HasConnection(x, y) == false)
                        continue;
                    double arc1 = Math.PI / 5 * y;
                    double arc2 = Math.PI / 5 * x;

                    double x1 = MyGraphControl.ActualWidth / 2 + (MyGraphControl.ActualWidth / 2 - r / 2) * Math.Cos(arc1);
                    double y1 = MyGraphControl.ActualHeight / 2 + (MyGraphControl.ActualHeight / 2 - r / 2) * Math.Sin(arc1);

                    double x2 = MyGraphControl.ActualWidth / 2 + (MyGraphControl.ActualWidth / 2 - r / 2) * Math.Cos(arc2);
                    double y2 = MyGraphControl.ActualHeight / 2 + (MyGraphControl.ActualHeight / 2 - r / 2) * Math.Sin(arc2);

                    LineViewModel lineVM = new LineViewModel()
                    {
                        X1 = x1,
                        Y1 = y1,
                        X2 = x2,
                        Y2 = y2
                    };

                    vm.Connections.Add(lineVM);
                }

            MyGraphControl.VM = vm;
        }

        private void Loaded(object sender, RoutedEventArgs e)
        {
            Do();
        }

        private void CanvasResize(object sender, SizeChangedEventArgs e)
        {
            Do();
        }
    }
}

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

namespace Graphs.UserControls
{
    public delegate void OnTwoNodeClick(int node1, int node2);
    public delegate void OnLineClick(LineViewModel lineVM);
    /// <summary>
    /// Interaction logic for GraphControl.xaml
    /// </summary>
    public partial class GraphControl : UserControl
    {
        public GraphViewModel _vm;
        private const int NewCanvasPer = 150;
        CircleViewModel node1 = null, node2 = null;

        public OnTwoNodeClick OnTwoNodeClickEvent { get; set; }
        public OnLineClick OnLineClick { get; set; }

        public GraphViewModel VM
        {
            set
            {
                _vm = value;
                Draw();
                node1 = node2 = null;
            }
            protected get
            {
                return _vm;
            }
        }
        public GraphControl()
        {
            InitializeComponent();

        }

        public void DrawNodes()
        {
            int i = 0;
            Canvas canvas = MyCanvas;
            bool add = false;
            foreach (var node in VM.Nodes)
            {
                Circle circle = new Circle();
                circle.MouseDoubleClick += OnNodeDoubleClick;
                circle.DataContext = node;
                canvas.Children.Add(circle);

                if (i >= NewCanvasPer)
                {
                    if (add)
                        MyCanvas.Children.Add(canvas);

                    canvas = new Canvas();
                    i = 0;
                    add = true;
                }
                i++;
            }

            if (add)
                MyCanvas.Children.Add(canvas);
        }

        public void DrawConnections()
        {
            foreach (var connection in VM.Connections)
            {
                Line line = new Line();
                line.DataContext = connection;
                line.MouseDoubleClick += Line_MouseDoubleClick;
                MyCanvas.Children.Add(line);
            }
        }

        private void Line_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            LineViewModel vm = (sender as Line).DataContext as LineViewModel;
            OnLineClick(vm);
        }

        private void Draw()
        {
            Clear();
            DrawConnections();
            DrawNodes();

            if(VM is DirectedGraphViewModel)
            {
                DrawTriangles();
            }


        }

        private void DrawTriangles()
        {
            var vm = VM as DirectedGraphViewModel;
            foreach (var trinagle in vm.Triangles)
            {
                Triangle line = new Triangle();
                line.DataContext = trinagle;
                MyCanvas.Children.Add(line);
            }

        }

        private void Clear()
        {
            foreach(var child in MyCanvas.Children)
            {
                if(child is Circle)
                {
                    var circle = child as Circle;
                    circle.MouseDoubleClick -= OnNodeDoubleClick;
                }

                if(child is Canvas)
                {
                    foreach(var morechild in (child as Canvas).Children)
                    {
                        if (morechild is Circle)
                        {
                            var circle = morechild as Circle;
                            circle.MouseDoubleClick -= OnNodeDoubleClick;
                        }
                    }
                }
            }
            MyCanvas.Children.Clear();
        }

        private void OnNodeDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var circle = sender as Circle;
            var vm = circle.DataContext as CircleViewModel;

            if (node1 == null)
            {
                node1 = vm;
                vm.Selected = true;
                node1.AllChanged();
            }
            else if (node2 == null && vm.NodeNumber != node1.NodeNumber)
            {
                node2 = vm;
                if (OnTwoNodeClickEvent != null)
                    OnTwoNodeClickEvent(node1.NodeNumber, node2.NodeNumber);
                
            }
            else
            {
                if (node1 != null)
                {
                    node1.Selected = false;
                    node1.AllChanged();
                }
                node1 = node2 = null;
            }
        }
    }
}

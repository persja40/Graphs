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
    /// <summary>
    /// Interaction logic for GraphControl.xaml
    /// </summary>
    public partial class GraphControl : UserControl
    {
        public GraphViewModel _vm;
        public GraphViewModel VM
        {
            set
            {
                _vm = value;
                Draw();
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
            foreach(var node in VM.Nodes)
            {
                Circle circle = new Circle();
                circle.DataContext = node;
                MyCanvas.Children.Add(circle);
            }
        }

        public void DrawConnections()
        {
            foreach(var connection in VM.Connections)
            {
                Line line = new Line();
                line.DataContext = connection;
                MyCanvas.Children.Add(line);
            }
        }

        private void Resize(object sender, SizeChangedEventArgs e)
        {
            if (VM != null)
            {
                Draw();
            }
        }

        private void Draw()
        {
            Clear();
            DrawConnections();
            DrawNodes();
        }

        private void Clear()
        {
            MyCanvas.Children.Clear();
        }
    }
}

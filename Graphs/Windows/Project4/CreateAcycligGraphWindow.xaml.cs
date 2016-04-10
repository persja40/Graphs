using Graphs.Actions;
using Graphs.Data;
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

namespace Graphs.Windows.Project4
{
    /// <summary>
    /// Interaction logic for CreateAcycligGraphWindow.xaml
    /// </summary>
    public partial class CreateAcycligGraphWindow : Window
    {
        public CreateAcycligGraphWindow()
        {
            InitializeComponent();
        }

        private void Generate(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public DirectedGraphMatrix Generate()
        {
            AcyclicGraphGenerator generator = new AcyclicGraphGenerator();

            generator.RowCount = int.Parse(RowCount.Text);
            generator.MaxNodesPerRow = int.Parse(NodePerRowCount.Text);
            generator.NodePropability = double.Parse(NodePropability.Text) / 100.0;
            generator.NodeNeighbourJoinPropability = double.Parse(NeighbourPropability.Text) / 100.0;
            generator.NodeLongDistanceJoinProability = double.Parse(LongPropability.Text) / 100.0;

            return generator.Generate();
        }

        private void OnClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DataContext = Generate();
        }
    }
}

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

namespace Graphs.Windows.Project5
{
    /// <summary>
    /// Interaction logic for CreateFlowNetworkWindow.xaml
    /// </summary>
    public partial class CreateFlowNetworkWindow : Window
    {
        public CreateFlowNetworkWindow()
        {
            InitializeComponent();
        }

        private void Generate(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public GraphMatrix Generate()
        {
            RandomNetworkGraphCreator generator = new RandomNetworkGraphCreator();

            generator.RowCount = int.Parse(RowCount.Text);
            generator.NodePropability = double.Parse(NodePropability.Text) / 100.0;

            return generator.Generate();
        }

        private void OnClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DataContext = Generate();
        }
    }
}

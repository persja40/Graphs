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

namespace Graphs.Windows.Generators
{
    /// <summary>
    /// Interaction logic for SecondGenerator.xaml
    /// </summary>
    public partial class SecondGenerator : Window
    {
        public SecondGenerator()
        {
            InitializeComponent();
        }

        private void Generate(object sender, RoutedEventArgs e)
        {
            Close();
        }



        private GraphMatrix Generate()
        {
            int nodesCount;
            double propability;



            nodesCount = int.Parse(NodeCount.Text);
            propability = double.Parse(Propability.Text) / 100.0;

            GraphMatrix graph = GraphGenerator.generatorGnp(nodesCount, propability);
            //graph.generatorGER(nodesCount, connectionCount);

            return graph;
        }

        private void OnClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int temp;
            double temp2;
            if (int.TryParse(NodeCount.Text, out temp) && double.TryParse(Propability.Text, out temp2))
                DataContext = Generate();
        }
    }
}

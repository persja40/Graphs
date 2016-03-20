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

namespace Graphs.Windows.Project2
{
    /// <summary>
    /// Interaction logic for CreateRegularWindow.xaml
    /// </summary>
    /// 
    public partial class CreateRegularWindow : Window
    {
        public int NodesCount { get; set; } = 5;
        public CreateRegularWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        private void ClickClose(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(NodesCountTextBox))
                MessageBox.Show("Wrong input!");
            else
                this.Close();
        }
    }
}

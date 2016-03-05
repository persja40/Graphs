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
    /// Interaction logic for GraphListControlItem.xaml
    /// </summary>
    public partial class GraphListControlItem : UserControl
    {
        public GraphListControlItem()
        {
            InitializeComponent();
        }

        private void AddNewItem(object sender, RoutedEventArgs e)
        {
            GraphListItemViewModel vm = DataContext as GraphListItemViewModel;

            vm.ConnectedNodes.Add(-1);
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            ListViewItem local = ((sender as Button).Tag as ListViewItem);
            var dc = DataContext;
        }
    }
}

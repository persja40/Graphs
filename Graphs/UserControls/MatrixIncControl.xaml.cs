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
    /// Interaction logic for MatrixIncControl.xaml
    /// </summary>
    public partial class MatrixIncControl : UserControl
    {
        public MatrixIncControl()
        {
            InitializeComponent();
        }

        private void OnDataContextChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is MatrixIncViewModel)
            {
                var vm = DataContext as MatrixIncViewModel;
                prepareGrid(vm.NodeCount, vm.ConnectionCount);
                createLabels(vm);
                prepareLegend(vm);
            }
        }

        protected virtual void createLabels(MatrixIncViewModel vm)
        {
            for (int node = 0; node < vm.NodeCount; ++node)
                for (int connection = 0; connection < vm.ConnectionCount; ++connection)
                {
                    MatrixControlItem item = new MatrixControlItem();
                    var ivm = new MatrixItemViewModel()
                    {
                        Text = vm.Connections[node, connection].ToString(),
                        Background = vm.Connections[node, connection] != 0
                        ? new SolidColorBrush(Color.FromArgb(50, 0, 255, 0)) : new SolidColorBrush(Colors.Transparent),
                        Visibility = Visibility.Collapsed,
                        Hint = string.Format("[{0}, {1}] - {2}", connection, node, vm.Connections[node, connection])
                    };
                    item.DataContext = ivm;
                    MatrixIncGrid.Children.Add(item);
                    Grid.SetRow(item, connection + 1);
                    Grid.SetColumn(item, node + 1);
                }
        }


        protected void prepareLegend(MatrixIncViewModel vm)
        {
            for (int x = 0; x < vm.NodeCount; ++x)
            {
                var Label = new MatrixControlItem();
                var ivm = new MatrixItemViewModel()
                {
                    Text = (x + 1).ToString(),
                    Hint = "",
                    Background = new SolidColorBrush(Colors.Gray),
                };
                Label.DataContext = ivm;

                MatrixIncGrid.Children.Add(Label);
                Grid.SetRow(Label, 0);
                Grid.SetColumn(Label, x + 1);
            }

            for (int y = 0; y < vm.ConnectionCount; ++y)
            {
                var Label = new MatrixControlItem();
                var ivm = new MatrixItemViewModel()
                {
                    Text = (y + 1).ToString(),
                    Hint = "",
                    Background = new SolidColorBrush(Colors.Gray)
                };
                Label.DataContext = ivm;
                MatrixIncGrid.Children.Add(Label);
                Grid.SetRow(Label, y + 1);
                Grid.SetColumn(Label, 0);
            }
        }

        protected virtual void prepareGrid(int nodesCount, int connectionCount)
        {
            MatrixIncGrid.Children.Clear();
            MatrixIncGrid.ColumnDefinitions.Clear();
            MatrixIncGrid.RowDefinitions.Clear();

            ColumnDefinition cd = new ColumnDefinition();
            cd.Width = new GridLength(1, GridUnitType.Auto);
            MatrixIncGrid.ColumnDefinitions.Add(cd);

            RowDefinition rd = new RowDefinition();
            rd.Height = new GridLength(1, GridUnitType.Auto);
            MatrixIncGrid.RowDefinitions.Add(rd);


            for (int i = 0; i < nodesCount; ++i)
            {
                cd = new ColumnDefinition();
                cd.Width = new GridLength(1, GridUnitType.Star);
                MatrixIncGrid.ColumnDefinitions.Add(cd);
            }

            for(int i = 0; i < connectionCount; ++i)
            {
                rd = new RowDefinition();
                rd.Height = new GridLength(1, GridUnitType.Star);
                MatrixIncGrid.RowDefinitions.Add(rd);
            }
        }
    }
}

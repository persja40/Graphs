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
    /// Interaction logic for MatrixControl.xaml
    /// </summary>
    public partial class MatrixControl : UserControl
    {
        public MatrixControl()
        {
            InitializeComponent();
        }

        private void OnDataContextChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(DataContext is MatrixViewModel)
            {
                var vm = DataContext as MatrixViewModel;
                prepareGrid(vm.NodeCount);
                createLabels(vm);
                prepareLegend(vm);
            }
        }

        protected virtual void prepareLegend(MatrixViewModel vm)
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

                MatrixGrid.Children.Add(Label);
                Grid.SetRow(Label, 0);
                Grid.SetColumn(Label, x + 1);
            }

            for (int y = 0; y < vm.NodeCount; ++y)
            {
                var Label = new MatrixControlItem();
                var ivm = new MatrixItemViewModel()
                {
                    Text = (y + 1).ToString(),
                    Hint = "",
                    Background = new SolidColorBrush(Colors.Gray)
                };
                Label.DataContext = ivm;
                MatrixGrid.Children.Add(Label);
                Grid.SetRow(Label, y + 1);
                Grid.SetColumn(Label, 0);
            }
        }

        protected virtual void createLabels(MatrixViewModel vm)
        {
            for (int y = 0; y < vm.NodeCount; ++y)
                for (int x = 0; x < vm.NodeCount; ++x)
                {
                    MatrixControlItem item = new MatrixControlItem();
                    var ivm = new MatrixItemViewModel()
                    {
                        Text = vm.Connections[x, y].ToString(),
                        Background = vm.Connections[x, y] != 0
                        ? new SolidColorBrush(Color.FromArgb(50, 0, 255, 0)) : new SolidColorBrush(Colors.Transparent),
                        Visibility = vm.NodeCount <= 7 ? Visibility.Visible : Visibility.Collapsed,
                        Hint = string.Format("[{0}, {1}] - {2}", x, y, vm.Connections[x, y])
                    };
                    item.DataContext = ivm;
                    MatrixGrid.Children.Add(item);
                    Grid.SetRow(item, y + 1);
                    Grid.SetColumn(item, x + 1);
                }
        }

        protected virtual void prepareGrid(int nodesCount)
        {
            MatrixGrid.Children.Clear();
            MatrixGrid.ColumnDefinitions.Clear();
            MatrixGrid.RowDefinitions.Clear();

            ColumnDefinition cd = new ColumnDefinition();
            cd.Width = new GridLength(1, GridUnitType.Auto);
            MatrixGrid.ColumnDefinitions.Add(cd);

            RowDefinition rd = new RowDefinition();
            rd.Height = new GridLength(1, GridUnitType.Auto);
            MatrixGrid.RowDefinitions.Add(rd);


            for (int i = 0; i < nodesCount; ++i)
            {
                cd = new ColumnDefinition();
                cd.Width = new GridLength(1, GridUnitType.Star);
                MatrixGrid.ColumnDefinitions.Add(cd);

                rd = new RowDefinition();
                rd.Height = new GridLength(1, GridUnitType.Star);
                MatrixGrid.RowDefinitions.Add(rd);
            }
        }
    }
}

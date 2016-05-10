using Graphs.Data;
using Graphs.UserControls;
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
using System.Windows.Shapes;

namespace Graphs.Windows.Project6
{
    /// <summary>
    /// Interaction logic for PercolationWindow.xaml
    /// </summary>
    public partial class PercolationWindow : Window
    {
        public PerlocationViewModel VM { get; set; }

        public PercolationWindow()
        {
            InitializeComponent();
        }

        public PercolationWindow(PerlocationViewModel percolation)
        {
            VM = percolation;

            InitializeComponent();

            Draw();
        }

        private void Draw()
        {
            for (int y = 0; y < VM.Size; ++y)
            {
                PerGrid.RowDefinitions.Add(new RowDefinition());
                for (int x = 0; x < VM.Size; ++x)
                {
                    if (y == 0)
                        PerGrid.ColumnDefinitions.Add(new ColumnDefinition());

                    var vm = VM.Squares[x, y];

                    Square square = new Square();
                    square.DataContext = vm;
                    square.SetValue(Grid.ColumnProperty, x);
                    square.SetValue(Grid.RowProperty, y);

                    PerGrid.Children.Add(square);


                }
            }
        }
    }
}

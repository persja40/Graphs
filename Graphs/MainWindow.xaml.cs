using Graphs.Data;
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

namespace Graphs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if(sender == Project1Button)
            {
                if (!Helpers.IsWindowOpen<Project1Window>())
                {
                    var window = new Project1Window();
                    window.Show();
                    return;
                }
            }
        }

        private void ShowAuthors(object sender, RoutedEventArgs e)
        {
            if(!Helpers.IsWindowOpen<Authors>())
            {
                var authorsWindows = new Authors();
                authorsWindows.Show();
            }
        }
    }
}

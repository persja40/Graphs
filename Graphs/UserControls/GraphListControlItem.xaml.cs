using Graphs.Misc;
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
using System.Windows.Threading;
using System.Collections.Specialized;

namespace Graphs.UserControls
{
    /// <summary>
    /// Interaction logic for GraphListControlItem.xaml
    /// </summary>
    public partial class GraphListControlItem : UserControl
    {

        public EventHandler<RoutedEventArgs> OnChange { get; set; }
        public GraphListControlItem()
        {
            InitializeComponent();
        }

        private void TextChange(object sender, TextCompositionEventArgs e)
         {
            /*Application.Current.Dispatcher.BeginInvoke(
              DispatcherPriority.Background,
              new Action(() =>
              {


                  if (OnChange != null)
                      OnChange(sender, e);
              }));
            */

        }

        private void dataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(DataContext is GraphListItemViewModel)
            {
                var dc = DataContext as GraphListItemViewModel;

                dc.OnChange += OnVMChange;
            }
        }

        private void OnVMChange()
        {
            OnChange(null, null);
        }
    }
}

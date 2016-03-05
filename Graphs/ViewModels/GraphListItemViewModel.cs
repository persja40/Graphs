using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.ViewModels
{
    public class GraphListItemViewModel
    {
        public int NodeNumer { get; set; }
        public ObservableCollection<int> ConnectedNodes { get; set; }

        public GraphListItemViewModel()
        {
            ConnectedNodes = new ObservableCollection<int>();
        }
    }
}

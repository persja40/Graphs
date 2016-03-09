using Graphs.Misc;
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
        public int NodeNumber { get; set; }
        public int NodeNumberForHuman { get { return NodeNumber + 1; } }
        public OnChange OnChange { get; set; }
        public ObservableCollection<int> _ConnectedNodes { get; set; }
        public ObservableCollection<int> ConnectedNodes
        {
            get
            {
                return _ConnectedNodes;
            }
            set
            {
                _ConnectedNodes = value;
                if (OnChange != null)
                    OnChange();
            }
        }

        public GraphListItemViewModel()
        {
            ConnectedNodes = new ObservableCollection<int>();
        }
    }
}

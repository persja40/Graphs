using Graphs.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.ViewModels
{
    public class GraphListViewModel
    {
        public ObservableCollection<GraphListItemViewModel> Items { get; set; }

        public GraphListViewModel()
        {
            Items = new ObservableCollection<GraphListItemViewModel>();
        }

        public GraphList GetList()
        {
            GraphList List = new GraphList(Items.Count);
            foreach(var item in Items)
            {
                var i = item.NodeNumber;
                foreach (var connection in item.ConnectedNodes)
                    if(List.GetConnection(connection, i))
                        List.MakeConnection(i, connection);
            }
            return List;
        }
    }
}

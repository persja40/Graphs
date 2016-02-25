using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.ViewModels
{
    public class GraphViewModel
    {
        public List<CircleViewModel> Nodes { get; set; }
        public List<LineViewModel> Connections { get; set; }
        public GraphViewModel()
        {
            Nodes = new List<CircleViewModel>();
            Connections = new List<LineViewModel>();
        }
    }
}

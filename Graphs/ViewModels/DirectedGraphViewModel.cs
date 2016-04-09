using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.ViewModels
{
    public class DirectedGraphViewModel : GraphViewModel
    {
        public List<TriangleViewModel> Triangles { get; set; } = new List<TriangleViewModel>();
    }
}

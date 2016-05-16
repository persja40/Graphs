using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.ViewModels
{
    public class DirectedWindowViewModel
    {
        public bool RegenerateGraphView { get; set; } = true;
        public bool RegenerateMatrix { get; set; } = false;
        public bool RegenerateMatrixInc { get; set; } = false;
        public bool RegenerateList { get; set; } = false;
        public bool ShowWeights { get; set; } = true;
    }
}
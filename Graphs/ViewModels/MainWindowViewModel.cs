using Graphs.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.ViewModels
{
    public class MainWindowViewModel
    {
        public bool RegenerateGraphView { get; set; } = true;
        public bool RegenerateMatrix { get; set; } = true;
        public bool RegenerateMatrixInc { get; set; } = true;
        public bool RegenerateList { get; set; } = true;
        public bool ShowWeights { get; set; } = true;
    }
}

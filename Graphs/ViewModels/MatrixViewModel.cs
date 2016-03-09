using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.ViewModels
{
    public class MatrixViewModel
    {
        public int[,] Connections { get; set; }
        public int NodeCount { get; set; }

        public MatrixViewModel(int nodesCount)
        {
            Connections = new int[nodesCount, nodesCount];
            NodeCount = nodesCount;
        }

        protected MatrixViewModel() { }
    }
}

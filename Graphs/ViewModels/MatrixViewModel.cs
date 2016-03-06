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
        public int Count { get; set; }

        public MatrixViewModel(int nodesCount)
        {
            Connections = new int[nodesCount, nodesCount];
            Count = nodesCount;
        }
    }
}

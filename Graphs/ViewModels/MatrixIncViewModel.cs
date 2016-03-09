using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.ViewModels
{
    public class MatrixIncViewModel : MatrixViewModel        
    {
        public int ConnectionCount { get; set; }

        public MatrixIncViewModel(int nodesCount, int connectionCount)
        {
            ConnectionCount = connectionCount;
            NodeCount = nodesCount;

            Connections = new int[nodesCount, connectionCount];
        }
    }
}

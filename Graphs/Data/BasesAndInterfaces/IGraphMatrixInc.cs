using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Data
{
    public interface IGraphMatrixInc : IGraph
    {
        bool GetConnectionArray(int node1, int conn);

        int ConnectNr { get; }
    }
}

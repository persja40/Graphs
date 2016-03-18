using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Data
{
    public interface IGraphList : IGraph
    {
        List<int> GetConnections(int node);
        
    }
}

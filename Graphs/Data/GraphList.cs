using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Data
{
    public class GraphList
    {
        public GraphList(int nodes)
        {
            connectNr = nodes;
            connect = new List<int>[nodes];
        }
        public void MakeConnection(int node1, int node2)
        {
            connect[node1].Add(node2);
            connect[node2].Add(node1);
        }
        public bool GetConnection(int node1, int node2)
        {
            return connect[node1].Contains(node2);
        }
        private List<int>[] connect;
        private int connectNr;
    }
}

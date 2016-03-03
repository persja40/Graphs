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
            nodesNr = nodes;
            connect = new List<int>[nodes];
        }
        public void MakeConnection(int node1, int node2)//tworzy w dwie strony
        {
            connect[node1].Add(node2);
            connect[node2].Add(node1);
        }
        public void RemoveConnection(int node1, int node2)//usuwa w dwie strony
        {
            connect[node1].Remove(node2);
            connect[node2].Remove(node1);
        }
        public bool GetConnection(int node1, int node2)
        {
            return connect[node1].Contains(node2);
        }
        public int NodesNr
        {
            get
            {
                int x = nodesNr;
                return x;
            }
        }
        public int CountElem(int x)
        {
            return connect[x].Count;
        }
        private List<int>[] connect;
        private int nodesNr;
    }
}

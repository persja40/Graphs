using Graphs.Misc;
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
            for (int i = 0; i < connect.Length; ++i)
                connect[i] = new List<int>();
        }
        public List<int> GetConnections(int node)
        {
            List<int> _return = new List<int>();
            foreach (var item in connect[node])
                _return.Add(item);
            return _return;
        }
        public void MakeConnection(int node1, int node2)//tworzy w dwie strony
        {
            connect[node1].Add(node2);
            connect[node2].Add(node1);
            if (OnChange != null)
                OnChange();
        }
        public void RemoveConnection(int node1, int node2)//usuwa w dwie strony
        {
            connect[node1].Remove(node2);
            connect[node2].Remove(node1);
            if (OnChange != null)
                OnChange();
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
        public bool Equals(GraphList x)
        {
            if (x == null)
                return false;
            if (this.NodesNr != x.NodesNr)
                return false;
            for (int i = 0; i < nodesNr; i++)
                if (!this.GetConnections(i).Equals(x.GetConnections(i)))
                    return false;
            return true;
        }
        public OnChange OnChange { get; set; }
        private List<int>[] connect;
        private int nodesNr;
    }
}

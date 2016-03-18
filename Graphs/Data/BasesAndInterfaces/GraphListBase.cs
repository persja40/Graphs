using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Data
{
    public abstract class GraphListBase : GraphBase, IGraphList
    {
        public override bool GetConnection(int node1, int node2)
        {
            return connect[node1].Contains(node2);
        }

        public List<int> GetConnections(int node)
        {
            List<int> _return = new List<int>();
            foreach (var item in connect[node])
                _return.Add(item);
            return _return;
        }


        public bool Equals(IGraphList other)
        {
            if (other == null)
                return false;
            if (this.NodesNr != other.NodesNr)
                return false;
            for (int i = 0; i < nodesNr; i++)
                if (this.GetConnections(i).Count == other.GetConnections(i).Count)
                {
                    for (int k = 0; k < this.GetConnections(i).Count; k++)
                        if (this.GetConnections(i)[k] != other.GetConnections(i)[k])
                            return false;
                }
                else
                    return false;
            return true;
        }

        public int CountElem(int x)
        {
            return connect[x].Count;
        }

        protected List<int>[] connect;
    }
}

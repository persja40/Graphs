using Graphs.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Data
{
    public abstract class GraphBase : IGraph
    {
        protected int nodesNr;

        public int NodesNr
        {
            get
            {
                return nodesNr;
            }
            protected set
            {
                nodesNr = value;
            }
        }

        public OnChange OnChange { get; set; }

        public abstract bool GetConnection(int node1, int node2);
        public abstract void MakeConnection(int node1, int node2);
        public abstract void RemoveConnection(int node1, int node2);
    }
}

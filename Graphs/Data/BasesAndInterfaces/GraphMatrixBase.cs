using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Data
{
    public abstract class GraphMatrixBase : GraphBase, IGraphMatrix
    {
        public void Set(IGraphMatrix other)
        {
            NodesNr = other.NodesNr;
            connect = new int[NodesNr, NodesNr];

            for (int y = 0; y < NodesNr; ++y)
                for (int x = 0; x < NodesNr; ++x)
                {
                    if (other.GetConnection(x, y))
                        MakeConnection(x, y);
                }
        }

        public override bool GetConnection(int node1, int node2)
        {
            return connect[node1, node2] >= 1;
        }
        public void Clear()
        {
            for (int i = 0; i < nodesNr; ++i)
                for (int j = 0; j < nodesNr; ++j)
                    connect[i, j] = 0;
        }

        public bool Equals(IGraphMatrix other)
        {
            if (other == null)
                return false;
            if (this.NodesNr != other.NodesNr)
                return false;
            for (int i = 0; i < nodesNr; i++)
                for (int j = 0; j < nodesNr; j++)
                    if (this.GetConnection(i, j) != other.GetConnection(i, j))
                        return false;
            return true;
        }



        protected int[,] connect;

        public abstract int ConnectionCount { get; }
    }
}

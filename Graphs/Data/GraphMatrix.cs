using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Graphs.Misc;

namespace Graphs.Data
{
    public class GraphMatrix
    {
        public GraphMatrix(int nodes)
        {
            nodesNr = nodes;

            connect = new int[nodesNr, nodesNr];
        }
        public GraphMatrix(int nodes, int[,] connections)
        {
            nodesNr = nodes;
            connect = new int[nodesNr, nodesNr];
            for (int i = 0; i < nodesNr; i++)
                for (int j = 0; j < nodesNr; j++)
                    connect[i, j] = connections[i, j];
        }
        public void MakeConnection(int node1, int node2)
        {
            connect[node1, node2] = connect[node2, node1] = 1;
        }
        public void RemoveConnection(int node1, int node2)
        {
            connect[node1, node2] = connect[node2, node1] = 0;
            if (OnChange != null)
                OnChange();
        }
        public bool GetConnection(int node1, int node2)
        {
            return connect[node1, node2] >= 1;
        }
        public void Clear()
        {
            for (int i = 0; i < nodesNr; ++i)
                for (int j = 0; j < nodesNr; ++j)
                    connect[i, j] = 0;
        }
        public void Set(GraphMatrix other)
        {

            nodesNr = other.nodesNr;
            connect = new int[nodesNr, nodesNr];

            for (int y = 0; y < NodesNr; ++y)
                for (int x = 0; x < NodesNr; ++x)
                {
                    if (other.GetConnection(x, y))
                        MakeConnection(x, y);
                }
        }
        public GraphMatrix Randomize(int x = 100)
        {
            Random r = new Random();
            int nr = r.Next(x);
            throw new NotImplementedException();
        }
        public OnChange OnChange { get; set; }
        public int ConnectionCount
        {
            get
            {
                int _ret = 0;
                for (int i = 0; i < nodesNr; ++i)
                    for (int j = i; j < nodesNr; ++j)
                        if (GetConnection(i, j))
                            _ret++;
                return _ret;

            }
        }
        public int NodesNr
        {
            get
            {
                int x = nodesNr;
                return x;
            }
        }
        private int[,] connect;
        private int nodesNr;
    }
}

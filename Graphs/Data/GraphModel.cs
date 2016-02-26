using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Data
{
    public class GraphModel
    {
        int[,] connections;

        int _nodesCount;
        public int NodesCount
        {
            get
            {
                return _nodesCount;
            }
            private set
            {
                _nodesCount = value;
            }
        }

        public GraphModel(int nodesCount)
        {
            NodesCount = nodesCount;
            connections = new int[nodesCount, nodesCount];

            for (int x = 0; x < nodesCount; ++x)
                for (int y = 0; y < nodesCount; ++y)
                    connections[x, y] = 0;
        }

        public void AddConnection(int node1, int node2)
        {
            Contract.Ensures(node1 != node2);
            connections[node1, node2] = 1;
            connections[node2, node1] = 1;
        }
        
        public bool HasConnection(int node1, int node2)
        {
            return connections[node1, node2] == 1;
        }

    }
}
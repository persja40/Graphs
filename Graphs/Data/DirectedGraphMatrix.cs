using Graphs.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Data
{
    public class DirectedGraphMatrix : GraphMatrixBase
    {
        public DirectedGraphMatrix(int nodes)
        {
            nodesNr = nodes;
            connect = new int[nodesNr, nodesNr];
            weights = new int[nodesNr, nodesNr];
        }
        public DirectedGraphMatrix(int nodes, int[,] connections)
        {
            nodesNr = nodes;
            connect = new int[nodesNr, nodesNr];
            for (int i = 0; i < nodesNr; i++)
                for (int j = 0; j < nodesNr; j++)
                    connect[i, j] = connections[i, j];
            weights = new int[nodesNr, nodesNr];
        }
        public override void MakeConnection(int node1, int node2)
        {
            MakeConnection(node1, node2, 1);
        }
        public override void RemoveConnection(int node1, int node2)
        {
            connect[node1, node2] = 0;
            weights[node1, node2] = 0;

            if (OnChange != null)
                OnChange();
        }

        public void MakeConnection(int node1, int node2, int weight)
        {
            connect[node1, node2] = 1;
            weights[node1, node2] = weight;
        }

        public override int ConnectionCount
        {
            get
            {
                throw new NotImplementedException();

            }
        }
        
    }
}

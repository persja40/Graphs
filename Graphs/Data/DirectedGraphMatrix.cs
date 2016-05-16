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
            Current = new int[nodesNr, nodesNr];
            SkojarzenieKolor = new int[nodesNr];
        }

        public override void setWeight(int n1, int n2, int val)
        {
            weights[n1, n2] = val;
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
            Current[node1, node2] = 0;
            SkojarzenieKolor[node1] = 0;

            if (OnChange != null)
                OnChange();
        }

        public void MakeConnection(int node1, int node2, int weight)
        {
            connect[node1, node2] = 1;
            weights[node1, node2] = weight;
            Current[node1, node2] = 0;
            SkojarzenieKolor[node1] = 0;
        }

        public override bool GetConnection(int node1, int node2)
        {
            return connect[node1, node2] >= 1;
        }

        public int GetCurrent(int startNode, int endNode)
        {
            return Current[startNode, endNode];
        }

        public override int ConnectionCount
        {
            get
            {
                var connections = 0;
                for(int node1 = 0; node1 < NodesNr; ++node1)
                {
                    for(int node2 = 0; node2 < NodesNr;++node2)
                    {
                        if (GetConnection(node1, node2))
                            connections++;
                    }
                }

                return connections;

            }
        }

        public void Set(DirectedGraphMatrix other)
        {
            Current = new int[other.nodesNr, other.nodesNr];
            SkojarzenieKolor = new int[other.nodesNr];
            base.Set(other);
            Columns = other.Columns;
            for (int y = 0; y < NodesNr; ++y)
            {
                for (int x = 0; x < NodesNr; ++x)
                {
                    Current[x, y] = other.Current[x, y];
                    
                }
                SkojarzenieKolor[y] = other.SkojarzenieKolor[y];
            }

        }

        public int[,] Current;
        public int[] SkojarzenieKolor { get; set; }



    }
}

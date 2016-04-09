using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Graphs.Misc;
using Graphs.Actions;

namespace Graphs.Data
{
    public class GraphMatrix : GraphMatrixBase
    {
        public GraphMatrix(int nodes)
        {
            nodesNr = nodes;
            connect = new int[nodesNr, nodesNr];
            weights = new int[nodesNr, nodesNr];
        }
        public GraphMatrix(int nodes, int[,] connections)
        {
            nodesNr = nodes;
            connect = new int[nodesNr, nodesNr];
            weights = new int[nodesNr, nodesNr];
            for (int i = 0; i < nodesNr; i++)
                for (int j = 0; j < nodesNr; j++)
                {
                    connect[i, j] = connections[i, j];
                    if (connections[i, j] == 1)
                        weights[i, j] = 1;
                }
        }
        public override void MakeConnection(int node1, int node2)
        {
            weights[node2, node1] = weights[node1, node2] = connect[node1, node2] = connect[node2, node1] = 1;
        }
        public override void RemoveConnection(int node1, int node2)
        {
            connect[node1, node2] = connect[node2, node1] = weights[node2, node1] = weights[node1, node2] = 0;
            if (OnChange != null)
                OnChange();
        }

        public static implicit operator GraphList(GraphMatrix matrix)
        {
            return Converter.ConvertToList(matrix);
        }

        public static implicit operator GraphMatrixInc(GraphMatrix matrix)
        {
            return Converter.ConvertToMatrixInc(matrix);
        }
        public static GraphMatrix Free(GraphMatrix f)//czysci wolne wezly
        {
            List<int> z = new List<int>();
            for (int i = 0; i < f.NodesNr; i++)
            {
                int suma = 0;
                for (int j = 0; j < f.NodesNr; j++)
                    if (f.GetConnection(i, j))
                        suma++;
                if (suma == 0)
                    z.Add(i);
            }
            GraphMatrix w = new GraphMatrix(f.NodesNr - z.Count);
            int k = 0;
            int l = 0;//litera ;)
            for (int i = 0; i < f.NodesNr; i++)
            {
                if (z.Contains(i))
                    continue;
                l = 0;
                for (int j = 0; j < f.NodesNr; j++)
                {
                    if (z.Contains(j))
                        continue;
                    if (f.GetConnection(i, j))
                    {
                        w.MakeConnection(k, l);
                        w.setWeight(k, l, f.getWeight(i, j));
                    }
                    l++;
                }
                k++;
            }
            return w;
        }
        public override int ConnectionCount
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
    }

}

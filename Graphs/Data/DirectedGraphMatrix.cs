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
        }
        public DirectedGraphMatrix(int nodes, int[,] connections)
        {
            nodesNr = nodes;
            connect = new int[nodesNr, nodesNr];
            for (int i = 0; i < nodesNr; i++)
                for (int j = 0; j < nodesNr; j++)
                    connect[i, j] = connections[i, j];
        }
        public override void MakeConnection(int node1, int node2)
        {
            MakeConnection(node1, node2, 1);
        }
        public override void RemoveConnection(int node1, int node2)
        {
            connect[node1, node2] = 0;

            //TODO : Remove any weights

            if (OnChange != null)
                OnChange();
        }

        public void MakeConnection(int node1, int node2, int weight)
        {
            connect[node1, node2] = 1;
            throw new NotImplementedException();
        }

        public int GetWeight(int node1, int node2)
        {
            throw new NotImplementedException();
        }

        /*
        public static SGraphMatrix Free(SGraphMatrix f)//czysci wolne wezly
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
            SGraphMatrix w = new SGraphMatrix(f.NodesNr - z.Count);
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
                        w.MakeConnection(k, l);
                    l++;
                }
                k++;
            }
            return w;
        }
        */
        public override int ConnectionCount
        {
            get
            {
                throw new NotImplementedException();

            }
        }
        
    }
}

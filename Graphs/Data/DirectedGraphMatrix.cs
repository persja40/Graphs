using Graphs.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Data
{
    public class SGraphMatrix
    {
        public SGraphMatrix(int nodes)
        {
            nodesNr = nodes;
            connect = new int[nodesNr, nodesNr];
        }
        public SGraphMatrix(int nodes, int[,] connections)
        {
            nodesNr = nodes;
            connect = new int[nodesNr, nodesNr];
            for (int i = 0; i < nodesNr; i++)
                for (int j = 0; j < nodesNr; j++)
                    connect[i, j] = connections[i, j];
        }
        public void MakeConnection(int node1, int node2)
        {
            connect[node1, node2] = 1;
        }
        public void RemoveConnection(int node1, int node2)
        {
            connect[node1, node2] = 0;
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
        public void Set(SGraphMatrix other)
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
        public OnChange OnChange { get; set; }
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
        public bool Equals(SGraphMatrix x)
        {
            if (x == null)
                return false;
            if (this.NodesNr != x.NodesNr)
                return false;
            for (int i = 0; i < nodesNr; i++)
                for (int j = 0; j < nodesNr; j++)
                    if (this.GetConnection(i, j) != x.GetConnection(i, j))
                        return false;
            return true;
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

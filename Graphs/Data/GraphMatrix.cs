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
            int[] a = new int[nodesNr];
            Random r = new Random();
            GraphList temp = Converter.ConvertToList(this);
            for (int i = 0; i < x; i++)
            {
                a[0] = r.Next(nodesNr);
                a[1] = r.Next(nodesNr);
                a[2] = r.Next(nodesNr);
                a[3] = r.Next(nodesNr);
                if (temp.GetConnection(a[0], a[1]) && temp.GetConnection(a[2], a[3]) && !(temp.GetConnection(a[0], a[3])) && !(temp.GetConnection(a[1], a[2])))
                {
                    temp.RemoveConnection(a[0], a[1]);
                    temp.RemoveConnection(a[2], a[3]);
                    temp.MakeConnection(a[0], a[3]);
                    temp.MakeConnection(a[1], a[2]);
                }
            }
            return Converter.ConvertToMatrix(temp);
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

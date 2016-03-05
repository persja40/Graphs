using Graphs.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Data
{
    public class GraphMatrixInc
    {
        public GraphMatrixInc(int nodes, int cons)
        {
            nodesNr = nodes;
            connectNr = cons;
            connect = new int[nodesNr, connectNr];
        }
        public GraphMatrixInc(int nodes, int cons, int[,] arr)
        {
            nodesNr = nodes;
            connectNr = cons;
            connect = new int[nodesNr, connectNr];
            for (int i = 0; i < nodesNr; i++)
                for (int j = 0; j < connectNr; j++)
                    connect[i, j] = arr[i, j];
        }
        public void MakeConnection(int node1, int node2, int con1)
        {
            connect[node1, con1] = 1;
            connect[node2, con1] = 1;
            if (OnChange != null)
                OnChange();
        }
        public bool GetConnection(int node1, int node2)
        {
            if (node1 == node2)
                return false;
            for (int i = 0; i < connectNr; i++)
                if ((connect[node1, i] == 1) && (connect[node2, i] == 1))
                    return true;
            return false;
        }
        public int NodesNr
        {
            get
            {
                int x = nodesNr;
                return x;
            }
        }
        public int ConnectNr
        {
            get
            {
                int x = connectNr;
                return x;
            }
        }
        public OnChange OnChange { get; set; }
        internal int[,] connect;//uwaga tablica x*y przy czym x-wezly, y-polaczenia
        internal int nodesNr;
        internal int connectNr;

        public override string ToString()
        {
            var str= 
                "NodesNr = " + NodesNr + Environment.NewLine +
                 "ConnectNr = " + ConnectNr + Environment.NewLine +
                 "connect = " + Environment.NewLine;

            for (int i = 0; i < nodesNr; ++i)
                for (int j = 0; j < ConnectNr; ++j)
                    str += string.Format("[{0},{1}] = {2}{3}", i, j, connect[i, j], Environment.NewLine);

            return str;

        }
    }
}

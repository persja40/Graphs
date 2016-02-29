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
            connect = arr;
        }
        public void MakeConnection(int node1, int node2, int con1)
        {
            connect[node1,con1]=connect[node2,con1]=1;
        }
        public bool GetConnection(int node1, int node2)
        {
            return connect[node1, node2] >= 1;
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
        private int[,] connect;//uwaga tablica x*y przy czym x-wezly, y-polaczenia
        private int nodesNr;
        private int connectNr;
    }
}

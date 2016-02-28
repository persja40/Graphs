using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Data
{
    public class GraphMatrixInc
    {
        public bool GetConnection(int node1, int node2) {
            return false;//UZUPELNIC
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

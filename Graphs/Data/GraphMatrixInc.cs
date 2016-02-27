using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Data
{
    public class GraphMatrixInc
    {
        public int[,] Connect
        {
            get
            {
                return connect;
            }
            set
            {
                connect = value;
            }
        }
        public int NodesNr
        {
            get
            {
                return nodesNr;
            }
            set
            {
                nodesNr = value;
            }
        }
        public int ConnectNr
        {
            get
            {
                return connectNr;
            }
            set
            {
                connectNr = value;
            }
        }
        private int[,] connect;//uwaga tablica x*y przy czym x-wezly, y-polaczenia
        private int nodesNr;
        private int connectNr;
    }
}

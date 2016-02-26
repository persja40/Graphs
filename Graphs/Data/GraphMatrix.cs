using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Data
{
    public class GraphMatrix{
        public GraphMatrix(int nr){
            nodesNr = nr;
            connect = new int[nr, nr];
        }

        private int[,] connect;
        private int nodesNr;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Graphs.Data
{
    public class GraphMatrix
    {
        public void generator1(int nodes, int branches)
        {
            nodesNr = nodes;
            connect = new int[nodes, nodes];//default =0
            //TU OSTATNIO


        }
        public void generator2(int nodes, double prob)
        {
            if (0 <= prob || prob <= 1 || nodes < 2)
                return;
            nodesNr = nodes;
            connect = new int[nodes, nodes];//default =0
            Random r = new Random();
            for (int i = 1; i < nodesNr; i++)
                for (int j = 1; j < i; j++)
                    if (r.Next(10000000) / 10000000 >= prob)
                        connect[i, j] = connect[j, i] = 1;
        }
        public void readFromFile() {
            //???
        }
        private int[,] connect;
        private int nodesNr;
    }
}

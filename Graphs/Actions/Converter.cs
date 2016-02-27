using Graphs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Actions
{
    public static class Converter
    {
        public static GraphMatrix ConvertToMatrix(GraphMatrixInc from)
        {
            int max = from.NodesNr;
            int[,] tab = new int[max, max];
            Pair<int, int> a = new Pair<int, int>;
            for (int j = 0; j < from.ConnectNr; j++)
            {
                a.First = a.Second = -1;
                int i = 0;
                while (a.Second == -1)
                {
                    if (from.Connect[i, j] == 1)
                    {
                        if (a.First == -1)
                            a.First = 1;
                        else
                        {
                            a.Second = 1;
                            break;
                        }
                    }
                    i++;
                }
                tab[a.First, a.Second] = tab[a.Second, a.First] = 1;
            }
            GraphMatrix x = new GraphMatrix(max, tab);
            return x;
        }
        public static GraphMatrixInc ConvertToMatrixInc(GraphList from)
        {
            throw new NotImplementedException();
        }

        public static GraphList ConvertToList(GraphMatrix from)
        {
            throw new NotImplementedException();
        }


    }

    class Pair<T1, T2>
    {
        public T1 First { get; set; }
        public T2 Second { get; set; }
    }
}

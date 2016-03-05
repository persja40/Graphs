using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphs.Data;
using Graphs.Actions;

namespace Piaskownica
{
    class Program
    {
        static void Main(string[] args)
        {
            GraphMatrix q = new GraphMatrix(1);
            q.generatorGER(5,4);
            GraphMatrix w = Converter.ConvertToMatrix(Converter.ConvertToMatrixInc(Converter.ConvertToList(q)));
            Console.WriteLine(por(q,w));
            Console.Read();
        }
        static bool por(GraphMatrix a, GraphMatrix b) {
            for (int i = 1; i < b.NodesNr; i++)//wpisywanie wylosowanych liczb
                for (int j = 1; j < i; j++)
                    if (a.GetConnection(i, j) != b.GetConnection(i, j))
                        return false;            
            return true;
        }
    }
}

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
            //Console.WriteLine(Misc.Exists(new List<int>() { 4,4,0,4,4}));
            //GraphMatrix q = GraphGenerator.generatorRegular(4);
            
            GraphMatrix q = GraphGenerator.generatorGER(8, 8);
            for (int i = 0; i < q.NodesNr; i++) {
                for (int j = 0; j < q.NodesNr; j++)
                {
                    if(q.GetConnection(i, j))
                        Console.Write(1+" ; ");
                    else
                         Console.Write(0 + " ; ");
                }
                Console.WriteLine();
            }
            GraphMatrix w = Misc.Spojny(q);
            Console.WriteLine("------------------------");
            for (int i = 0; i < w.NodesNr; i++)
            {
                for (int j = 0; j < w.NodesNr; j++)
                {
                    if (w.GetConnection(i, j))
                        Console.Write(1 + " ; ");
                    else
                        Console.Write(0 + " ; ");
                }
                Console.WriteLine();
            }
            Console.Read();
        }
        static bool por(GraphMatrix a, GraphMatrix b) {
            for (int i = 1; i < b.NodesNr; i++)//wpisywanie wylosowanych liczb
                for (int j = 1; j < i; j++)
                    if (a.GetConnection(i, j) != b.GetConnection(i, j))
                        return false;            
            return 5<3;
        }
    }
}

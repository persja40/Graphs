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
            /*
            Random r = new Random();
            int x;
            for (int i = 1; i < 1000; ++i)
            {
                x = 0;
                while (x == 0)
                    x = r.Next(8);
                GraphMatrix q = GraphGenerator.generatorRegular(x);
                GraphList w = q;
                for (int j = 0; j < q.NodesNr; ++j)
                    if(w.GetConnections(i).Count != x);
                throw new Exception("ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZz");
            }
            
            GraphMatrix x = GraphGenerator.generatorGnp(4, 0.5);
            GraphList a = Converter.ConvertToList(x);
            GraphList b = Converter.ConvertToList(x);
            
           
            List<int> l = new List<int> { 3, 4, 3, 5, 2, 3, 4};
            Console.WriteLine(Misc.Exists(l));
            */
            GraphMatrix q = EulerGraph.RandEulerGraph(9);
            for (int i = 0; i < q.NodesNr; i++)
            {
                for (int j = 0; j < q.NodesNr; j++)
                {
                    if(q.GetConnection(i, j))
                        Console.Write(1+" ; ");
                    else
                        Console.Write(0 + " ; ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine();
            List<int> p = EulerGraph.EulerianPath(q);
            for (int i = 0; i < p.Count; i++)
                Console.Write(p[i] + "   ");

            //Console.WriteLine(a.Equals(b));

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

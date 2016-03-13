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
<<<<<<< HEAD
            

            List<int> l = new List<int> { 2, 2, 2, 2, 2, 2, 2 };

            
            GraphMatrix x = GraphGenerator.generatorGnp(4, 0.5);
            GraphList a = Converter.ConvertToList(x);
            GraphList b = Converter.ConvertToList(x);
            

            List<int> l = new List<int> { 2, 2, 2, 2, 2, 2, 2 };
            Console.WriteLine(Misc.Exists(l));
            //GraphMatrix q = Misc.Construct(l);
            GraphMatrix q = GraphGenerator.generatorRegular(2);
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
            */

            Random r = new Random();
            int x;
            for (int i = 1; i < 100; ++i)
            {
                x = 0;
                while (x == 0)
                    x = r.Next(8);
                //GraphMatrix q = GraphGenerator.generatorRegular(x);
                //GraphList w = q;
                List<int> l = new List<int> { 6,6,6,6,6,6,6,6,6,6 };
                Console.WriteLine(Misc.Exists(l));
                GraphMatrix q = Misc.Construct(l);
                for (int k = 0; k < q.NodesNr; k++)//test
                {
                    for (int p = 0; p < q.NodesNr; p++)
                    {
                        if (q.GetConnection(k, p))
                            Console.Write(1 + " ; ");
                        else
                            Console.Write(0 + " ; ");
                    }
                    Console.WriteLine();
                }
                Console.Read();
                /*
                for (int j = 0; j < w.NodesNr; ++j)
                    if (w.GetConnections(j).Count != x) {
                        Console.WriteLine("x: "+x);
                        Console.WriteLine(w.NodesNr);
                        for (int k = 0; k < q.NodesNr; k++)
                        {
                            for (int p = 0; p < q.NodesNr; p++)
                            {
                                if (q.GetConnection(k, p))
                                    Console.Write(1 + " ; ");
                                else
                                    Console.Write(0 + " ; ");
                            }
                            Console.WriteLine();
                        }
                        Console.Read();
                    }
                */
            }

            Console.Read();
        }//main
        static bool por(GraphMatrix a, GraphMatrix b) {
            for (int i = 1; i < b.NodesNr; i++)//wpisywanie wylosowanych liczb
                for (int j = 1; j < i; j++)
                    if (a.GetConnection(i, j) != b.GetConnection(i, j))
                        return false;            
            return 5<3;
        }
    }
}

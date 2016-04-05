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
            /*GraphMatrix w = GraphGenerator.generatorRegular(3, 7);
            //GraphMatrix w =  EulerGraph.RandEulerGraph(13);
            //GraphMatrix q = GraphGenerator.Randomize(w);
            w = GraphGenerator.CreateRandomWeights(w);
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
            Console.WriteLine("------------------------------");
            for (int i = 0; i < w.NodesNr; i++)
            {
                for (int j = 0; j < w.NodesNr; j++)
                {
                        Console.Write(w.getWeight(i,j) + " ; ");
                }
                Console.WriteLine();
            }
            GraphMatrix q = GraphGenerator.Prim(w);
            Console.WriteLine("------------------------------");

            for (int i = 0; i < q.NodesNr; i++)
            {
                for (int j = 0; j < q.NodesNr; j++)
                {
                    if (q.GetConnection(i, j))
                        Console.Write(1 + " ; ");
                    else
                        Console.Write(0 + " ; ");
                }
                Console.WriteLine();
            }*/

            List<int> degrees = new List<int>() { 4, 3, 5, 3, 3, 3, 3 };
            GraphMatrix g = Misc.Construct(degrees);
            //int[,] dist = Misc.distancesMatrix(g);
          /*  int rowLength = dist.GetLength(0);
            int colLength = dist.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", dist[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }*/

            /*
            List<int> q = EulerGraph.EulerianPath(w);
            Console.WriteLine("------------------------------");
            for (int i = 0; i < q.Count; i++)
            {
                Console.Write(q[i] + "   ");
            }
            Console.WriteLine();
            Console.WriteLine("------------------------------");
            GraphList e = w;
            for (int i = 0; i < e.NodesNr; i++)
            {
                Console.Write(e.GetConnections(i).Count + "   ");
            }
            */
            // List<List<int>> lista = Directed.circuts(q);
            //SGraphMatrix e = Directed.Smaxspojny(q,lista);
            /*
            for (int i = 0; i < lista.Count; i++)
            {
                for (int j = 0; j < lista[i].Count; j++)
                {
                    Console.Write(lista[i][j]+"  ");
                }
                Console.WriteLine();
            }
            */
            //Console.WriteLine("aaa");
            //List<int> x = new List<int>() { 0, 1, 2, 3 };
            //Console.WriteLine(Directed.ujemnyCykl(q,GraphGenerator.CreateRandomDirectedWeights(q)));

            Console.Read();
        }
        static bool por(GraphMatrix a, GraphMatrix b)
        {
            for (int i = 1; i < b.NodesNr; i++)//wpisywanie wylosowanych liczb
                for (int j = 1; j < i; j++)
                    if (a.GetConnection(i, j) != b.GetConnection(i, j))
                        return false;
            return 5 < 3;
        }
    }
}

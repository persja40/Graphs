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
            //int x = 6;
            //int y = 4;
            //GraphMatrix e = GraphGenerator.generatorGER(7,10);
            //GraphMatrix w =  EulerGraph.RandEulerGraph(13);
            //GraphMatrix q = GraphGenerator.Randomize(w);
            //e = GraphGenerator.CreateRandomWeights(e);
            // DirectedGraphMatrix w = GraphGenerator.CreateDirect(e);
            //w = GraphGenerator.CreateRandomDirectedWeights(w);
            /*
            Percolation w = new Percolation(7, 0.5);
            for (int i = 0; i < w.size; i++)
            {
                for (int j = 0; j < w.size; j++)     
                        Console.Write(w.matrix[i,j] + " | ");
                Console.WriteLine();
            }
            Console.WriteLine("------------------------------");
            */


            List<int> degrees = new List<int>() { 4, 3, 5, 3, 3, 3, 3 };
            GraphMatrix g = Misc.Construct(degrees);
            GraphMatrix graph = Misc.CreateBiggestCoherent(g);
            DirectedGraphMatrix cgraph = GraphGenerator.CreateDirectional(graph);
            DirectedGraphMatrix dgraph = GraphGenerator.CreateRandomDirectedWeights(cgraph);
            
            int nodes = dgraph.NodesNr;
            int[,] floyd = new int[nodes, nodes];
            //int[,] johnson = new int[nodes, nodes];

            floyd = Directed.FloydWarshall(dgraph);
            //johnson = Directed.Johnson(dgraph);

            // DirectedGraphMatrix g1 = Directed.Directedmaxspojny(dgraph);
            // DirectedGraphMatrix ngraph = GraphGenerator.CreateRandomDirectedWeights(g1);
            
            int[,] distances = new int[nodes, nodes];
            distances = Directed.distancesDirectedMatrix(dgraph);
            
            Console.WriteLine("---------------FloydWarshall---------------");
            
            for (int i = 0; i < nodes; ++i)
            {
                for (int j = 0; j < nodes; ++j)
                {
                    Console.Write(floyd[i, j] + " | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("-------------------------------------------");
            /*
            List<int> lista = new List<int>();
            lista = PathFinding.Dijkstra(dgraph, 2, 5);
            if (lista == null)
                Console.WriteLine("Pusta lista");
            else if(lista.Count == 0)
            {
                Console.WriteLine("Nie ma drogi");
            }
            else
            {
                Console.WriteLine("Niepusta lista");
            }
            /*
            List<int> bel = new List<int>();
            bel = Directed.BellmanFord(dgraph, 3, 4);
            
            for (int i = 0; i < lista.Count; ++i)
            {
                Console.Write(lista[i] + ", ");
                
            }
            Console.WriteLine();
            */
            /*
            if (bel != null)
            {
                for (int i = 0; i < bel.Count; ++i)
                {
                    Console.Write(bel[i] + ", ");

                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("bel == null");
            }

            /*
            Console.WriteLine("---------------Johnson---------------");
            for (int i = 0; i < nodes; ++i)
            {
                for (int j = 0; j < nodes; ++j)
                {
                    Console.Write(johnson[i, j] + " | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("--------------------------------------");
            */
            Console.WriteLine("---------------DistancesMatrix---------------");
            for (int k = 0; k < nodes; ++k)
            {
                for (int l = 0; l < nodes; ++l)
                {
                    Console.Write(distances[k, l] + " | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("---------------------------------------------");

            Console.WriteLine("---------------ConnectionsMatrix---------------");
            for (int k = 0; k < nodes; ++k)
            {
                for (int l = 0; l < nodes; ++l)
                {
                    Console.Write(dgraph.getConnect(k, l) + " | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("---------------------------------------------");

            Console.WriteLine("---------------WeightsMatrix---------------");
            for (int k = 0; k < nodes; ++k)
            {
                for (int l = 0; l < nodes; ++l)
                {
                    Console.Write(dgraph.getWeight(k, l) + " | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("---------------------------------------------");
            /*
            Console.WriteLine("---------------ConnectionsMatrix---------------");
            for (int k = 0; k < nodes; ++k)
            {
                for (int l = 0; l < nodes; ++l)
                {
                    Console.Write(dgraph.getConnect(k, l) + " | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("---------------------------------------------");
            */


            //Console.WriteLine(w.matrix[0,3]);

            /*
            List<int> q = Directed.BellmanFord(w,x,y);
            if (q != null)
            {
                for (int i = 0; i < q.Count; i++)
                    Console.Write(q[i] + "  ");
                Console.WriteLine();
                Console.WriteLine(Directed.pathWeight(w, q));
            }
            else
            {
                Console.WriteLine("Brak sciezki");
            }
            */
            //List<int> degrees = new List<int>() { 4, 3, 5, 3, 3, 3, 3 };
            //GraphMatrix g = Misc.Construct(degrees);
            //int[,] dist = Misc.distancesMatrix(g);
            /*
              int rowLength = dist.GetLength(0);
              int colLength = dist.GetLength(1);

              for (int i = 0; i < rowLength; i++)
              {
                  for (int j = 0; j < colLength; j++)
                  {
                      Console.Write(string.Format("{0} ", dist[i, j]));
                  }
                  Console.Write(Environment.NewLine + Environment.NewLine);
              }
            */

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
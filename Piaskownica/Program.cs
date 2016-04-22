using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphs.Data;
using Graphs.Actions;
using System.IO;

namespace Piaskownica
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            for (int i = 0; i < 500; i++)
            {
                Hamilton.HB();
                Console.WriteLine(i);
            }
            */
            // List<int> degrees = new List<int>() { 4, 3, 5, 3, 3, 2, };
            List<int> degrees = new List<int>() { 3, 3, 2, 2, 2 };
            GraphMatrix g = Misc.Construct(degrees);
            GraphMatrix graph = Misc.CreateBiggestCoherent(g);
            DirectedGraphMatrix cgraph = GraphGenerator.CreateDirectional(graph);
            DirectedGraphMatrix dgraph = GraphGenerator.CreateRandomDirectedWeights(cgraph);

            int nodes = dgraph.NodesNr;
            int[,] floyd = new int[nodes, nodes];
            int[,] johnson = new int[nodes, nodes];

            

            int[,] tab = new int[8, 8];
            tab[0, 2] = 1;
            tab[0, 4] = 1;
            tab[1, 2] = 1;
            tab[2, 0] = 1;
            tab[3, 1] = 1;
            tab[3, 5] = 1;
            tab[4, 7] = 1;
            tab[7, 6] = 1;
            DirectedGraphMatrix test = new DirectedGraphMatrix(8, tab);
            test.setWeight(0, 2, 3);
            test.setWeight(0, 4, 9);
            test.setWeight(1, 2, 5);
            test.setWeight(2, 0, 9);
            test.setWeight(3, 1, 3);
            test.setWeight(3, 5, 2);
            test.setWeight(4, 7, 1);
            test.setWeight(7, 6, 2);

            int[,] tab1 = new int[4, 4];
            tab1[0, 1] = 1;
            tab1[0, 2] = 1;
            tab1[0, 3] = 1;
            tab1[1, 2] = 1;
            tab1[2, 3] = 1;
            DirectedGraphMatrix test1 = new DirectedGraphMatrix(4, tab1);
            test1.setWeight(0, 1, -5);
            test1.setWeight(0, 2, 2);
            test1.setWeight(0, 3, 3);
            test1.setWeight(1, 2, 4);
            test1.setWeight(2, 3, 1);


            floyd = Directed.FloydWarshall(test1);
            johnson = Directed.Johnson(test1);

            // DirectedGraphMatrix g1 = Directed.Directedmaxspojny(dgraph);
            // DirectedGraphMatrix ngraph = GraphGenerator.CreateRandomDirectedWeights(g1);

            //int[,] distances = new int[nodes, nodes];
            //distances = Directed.distancesDirectedMatrix(dgraph);

            Console.WriteLine("---------------FloydWarshall---------------");

            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    Console.Write(floyd[i, j] + " | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("---------------Johnson---------------");
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    Console.Write(johnson[i, j] + " | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("--------------------------------------");

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
            */
            Console.WriteLine("---------------ConnectionsMatrix---------------");
            for (int k = 0; k < 4; ++k)
            {
                for (int l = 0; l < 4; ++l)
                {
                    Console.Write(test1.getConnect(k, l) + " | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("---------------------------------------------");

            Console.WriteLine("---------------WeightsMatrix---------------");
            for (int k = 0; k < 4; ++k)
            {
                for (int l = 0; l < 4; ++l)
                {
                    Console.Write(test1.getWeight(k, l) + " | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("---------------------------------------------");

            

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
            */
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
            Dictionary<int, Tuple<int, int>> points = Hamilton.LoadPoints(@"c:\dane.txt");
            List<int> best;
            for (int i = 0; i < 100; i++)
            {
                best = Hamilton.ClosestList(points,i+1);
                Hamilton.SavePoints(@"d:\wynik.txt", best, points);
                Console.WriteLine(i);
            }
            */
            //Hamilton.HB();
            //Find();
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
        static void Find()
        {
            StreamReader sr = new StreamReader(@"d:\wynik.txt");
            String line;
            bool parz = true;
            double max = 100000;
            while ((line = sr.ReadLine()) != null)
            {
                if (parz)
                {
                    if (max > Double.Parse(line))
                        max = Double.Parse(line);
                    //Console.WriteLine(line);
                }
                parz = !parz;
            }
            Console.WriteLine(max);
        }
    }
}
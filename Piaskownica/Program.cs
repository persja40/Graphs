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
            // List<int> degrees = new List<int>() { 4, 4, 4, 4, 4, 2};
            //List<int> degrees = new List<int>() { 3, 3, 2, 2, 2 };

            // GraphMatrix g = GraphGenerator.generatorGER(8, 11);
            //GraphMatrix graph = Misc.CreateBiggestCoherent(g);
            // DirectedGraphMatrix cgraph = GraphGenerator.CreateDirectional(g);
            // DirectedGraphMatrix spojny = Directed.Directedmaxspojny(cgraph);
            // DirectedGraphMatrix dgraph = GraphGenerator.CreateRandomDirectedWeights(spojny);
            /*
            Percolation w = new Percolation(7, 0.5);
            for (int i = 0; i < w.size; i++)
            {
                for (int j = 0; j < w.size; j++)
                    Console.Write(w.matrix[i, j] + " | ");
                Console.WriteLine();
            }


            int[,] con = new int[7, 7];
            con[0, 1] = 1;
            con[0, 4] = 1;
            con[1, 2] = 1;
            con[1, 3] = 1;
            con[2, 3] = 1;
            con[2, 6] = 1;
            con[3, 5] = 1;
            con[3, 6] = 1;
            con[4, 3] = 1;
            con[4, 5] = 1;
            con[5, 6] = 1;

            //con[0, 1] = 1;

            DirectedGraphMatrix siec = new DirectedGraphMatrix(7, con);

            siec.setWeight(0, 1, 9);
            siec.setWeight(0, 4, 9);
            siec.setWeight(1, 2, 7);
            siec.setWeight(1, 3, 3);
            siec.setWeight(2, 3, 4);
            siec.setWeight(2, 6, 6);
            siec.setWeight(3, 5, 2);
            siec.setWeight(3, 6, 9);
            siec.setWeight(4, 3, 3);
            siec.setWeight(4, 5, 6);
            siec.setWeight(5, 6, 8);
            
            Console.WriteLine("---------------getWeight---------------");

            for (int i = 0; i < 7; ++i)
            {
                for (int j = 0; j < 7; ++j)
                {
                    Console.Write(siec.getWeight(i, j) + " | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("-------------------------------------------");
            

            MaxFlow1 maxflow = new MaxFlow1(siec);
            int max = maxflow.findMaxFlow(siec);
            Console.WriteLine("MaxFlow: " + max);
            */
            // int nodes = dgraph.NodesNr;
            //int[,] floyd = new int[nodes, nodes];
            //int[,] johnson = new int[nodes, nodes];


            //floyd = Directed.FloydWarshall(dgraph);
            // johnson = Directed.Johnson(dgraph);
            /*
             LinkedList<int> list = new LinkedList<int>();
             list.AddLast(1); list.AddLast(2); list.AddLast(3); list.AddLast(4); list.AddLast(5); list.AddLast(6); list.AddLast(7);

             int first = list.First();
             int last = list.Last();

             Console.WriteLine("First:" +  first);
             Console.WriteLine("Last:" + last);
             list.AddLast(24);
             Console.WriteLine("Last: " + list.Last() + "\nlist.Count " + list.Count);
             list.RemoveFirst();
             Console.WriteLine("First: " + list.First() + "\nlist.Count " + list.Count);

             List<int> temp = new List<int>();
             temp.Add(1); temp.Add(2); temp.Add(3); temp.Add(4); temp.Add(5); temp.Add(6); temp.Add(7);
             temp.RemoveAt(temp.Count - 1);
             Console.WriteLine("Last: " + temp.Last());
             temp.Add(23);
             Console.WriteLine("Last: " + temp.Last() + "\ntemp.Count " + temp.Count);
             */
            // DirectedGraphMatrix g1 = Directed.Directedmaxspojny(dgraph);
            // DirectedGraphMatrix ngraph = GraphGenerator.CreateRandomDirectedWeights(g1);

            //int[,] distances = new int[nodes, nodes];
            //distances = Directed.distancesDirectedMatrix(dgraph);
            /*
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
            Percolation w = new Percolation(7, 0.5);
            for (int i = 0; i < w.size; i++)
            {
                for (int j = 0; j < w.size; j++)
                    Console.Write(w.matrix[i, j] + " | ");
                Console.WriteLine();
            }
            Console.WriteLine(w.max.Item1 + " wystepuje " + w.max.Item2);
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
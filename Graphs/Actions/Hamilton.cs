using Graphs.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Actions
{
    public class Hamilton
    {
        /*
        private static HashSet<int> closedEdges;
        private static HashSet<int> openNodes;
        private static HashSet<int> visitedNodes;

        private static int currentNode;
        private static int endNode;
        */

        /// <summary>
        /// Reads from file - Burda's points
        /// </summary>
        /// <param name="path">path to file</param>
        /// <returns> Dictionary Key->nr; arg-> x, y </returns>
        public static Dictionary<int, Tuple<int, int>> LoadPoints(string path)
        {
            Dictionary<int, Tuple<int, int>> points = new Dictionary<int, Tuple<int, int>>();
            if (!File.Exists(path))
                throw new Exception("File does not exist");
            StreamReader sr = new StreamReader(path);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split(' ');
                points.Add(Int32.Parse(arr[0]), new Tuple<int, int>(Int32.Parse(arr[1]), Int32.Parse(arr[2])));
            }
            //for (int i = 1; i <= 100; i++)
            //    Console.WriteLine(i.ToString() + "->  " + points[i].Item1 + " : " + points[i].Item2);
            return points;
        }

        /// <summary>
        /// Counts weight of path
        /// </summary>
        /// <param name="list">contains all elemnts exactly one time</param>
        /// <param name="points"></param>
        /// <returns></returns>
        public static double Calc(List<int> list, Dictionary<int, Tuple<int, int>> points)
        {
            double x = 0;
            for (int i = 0; i < (list.Count - 1); i++)
                x += Math.Sqrt(Math.Pow(points[list[i]].Item1 - points[list[i + 1]].Item1, 2) + Math.Pow(points[list[i]].Item2 - points[list[i + 1]].Item2, 2));
            x += Math.Sqrt(Math.Pow(points[1].Item1 - points[points.Count].Item1, 2) + Math.Pow(points[100].Item2 - points[points.Count].Item2, 2));
            return x;
        }

        /// <summary>
        /// Save weight and points of path to file - appends
        /// </summary>
        /// <param name="path"></param>
        /// <param name="list"></param>
        /// <param name="waga"></param>
        public static void SavePoints(string path, List<int> list, Dictionary<int, Tuple<int, int>> points)
        {
            System.IO.StreamWriter wr = new System.IO.StreamWriter(path, true);
            wr.WriteLine(Calc(list, points));
            for (int i = 0; i < list.Count; i++)
                wr.Write(list[i] + " ");
            wr.WriteLine();
            wr.Close();
        }

        /// <summary>
        /// Generates random list of vertices
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static List<int> RandomList(Dictionary<int, Tuple<int, int>> points, ref Random r)
        {
            List<int> lista = new List<int>();
            for (int i = 1; i <= points.Count; i++)
                lista.Add(i);
            List<int> ret = new List<int>();
            for (int i = 1; i <= points.Count; i++)
            {
                int x = r.Next(lista.Count);
                ret.Add(lista[x]);
                lista.Remove(lista[x]);
            }
            return ret;
        }

        /// <summary>
        /// Nearest neighbour
        /// </summary>
        /// <param name="points"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static List<int> ClosestList(Dictionary<int, Tuple<int, int>> points, int start = 1)
        {
            List<int> lista = new List<int>();
            int max = points.Count;
            int curr = start;
            lista.Add(curr);
            for (int i = 0; i < max - 1; i++)
            {
                int closest = curr;
                double cost = double.MaxValue;
                for (int j = 1; j <= max; j++)
                {
                    double temp = Math.Sqrt(Math.Pow(points[curr].Item1 - points[j].Item1, 2) + Math.Pow(points[curr].Item2 - points[j].Item2, 2));
                    if (curr != j && !lista.Contains(j) && cost > temp)
                    {
                        closest = j;
                        cost = temp;
                    }
                }
                lista.Add(closest);
                curr = closest;
            }
            return lista;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 2nd nearest neighbour
        /// </summary>
        /// <param name="points"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static List<int> Closest2List(Dictionary<int, Tuple<int, int>> points, int start = 1)
        {
            List<int> lista = new List<int>();
            int max = points.Count;
            int curr = start;// 2nd max index
            lista.Add(curr);
            for (int i = 0; i < max - 1; i++)
            {
                int next = curr;
                int next2 = curr;
                double cost = double.MaxValue;// max value
                double cost2 = double.MaxValue;// 2nd max value
                for (int j = 1; j < max; j++)
                {
                    double temp = Math.Sqrt(Math.Pow(points[curr].Item1 - points[j].Item1, 2) + Math.Pow(points[curr].Item2 - points[j].Item2, 2));
                    if (curr != j && !lista.Contains(j))
                    {
                        if (cost > temp)
                        {
                            next2 = next;
                            next = j;
                            cost2 = cost;
                            cost = temp;
                        }
                        else if (cost2 > temp)
                        {
                            next2 = j;
                            cost2 = temp;
                        }
                    }
                }
                lista.Add(next2);
                curr = next2;
            }
            for (int i = 1; i <= max; i++)
                if (!lista.Contains(i))
                    lista.Add(i);
            if (!Test(lista))
                throw new NotImplementedException();
            return lista;
        }

        /// <summary>
        /// Mutate list
        /// swaps 2 random elements
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<int> Mutation(Dictionary<int, Tuple<int, int>> points, List<int> list, ref Random r)
        {
            List<int> cp = new List<int>();
            for (int k = 0; k < list.Count; k++)
                cp.Add(list[k]);
            int i = r.Next(100);
            int j = r.Next(100);
            int x = cp[i];
            cp[i] = cp[j];
            cp[j] = x;
            if (Calc(cp, points) > Calc(list, points))
                return list;
            return cp;
        }

        /// <summary>
        /// Creates new list from mum & dad lists
        /// </summary>
        /// <param name="mum"></param>
        /// <param name="dad"></param>
        /// <returns>child contains (0.25-0.75)mum genes</returns>
        public static List<int> Inherit(List<int> mum, List<int> dad, ref Random r)
        {
            List<int> dadc = new List<int>();
            for (int i = 0; i < dad.Count; i++)//deep copy
                dadc.Add(dad[i]);
            int mnr = r.Next(25, 75);//how much points from mum
            List<int> child = new List<int>();
            for (int i = 0; i < mnr; i++)
            {
                child.Add(mum[i]);
                dadc.Remove(mum[i]);
            }
            for (int i = 0; i < dadc.Count; i++)
                child.Add(dadc[i]);
            return child;
        }

        /// <summary>
        /// Generates new population
        /// </summary>
        /// <param name="lista"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        public static List<List<int>> NextPopulation(List<List<int>> lista, Dictionary<int, Tuple<int, int>> points, ref Random r)
        {
            List<double> cost = new List<double>();
            for (int i = 0; i < lista.Count; i++)//cost of each solution
                cost.Add(Calc(lista[i], points));
            double avg = cost.Average();
            int max = lista.Count;
            List<int> deleted = new List<int>();
            int j = 0;
            for (int i = 0; i < max; i++)//finding solution over average
                if (cost[i] < avg)
                    deleted.Add(i);
            List<List<int>> ret = new List<List<int>>();//children list
            if (deleted.Count == 0)
                return lista;

            for (int i = 0; i < max; i++)
                ret.Add(Inherit(lista[deleted[r.Next(deleted.Count)]], lista[deleted[r.Next(deleted.Count)]], ref r));
            return ret;
        }

        /// <summary>
        /// Gets optimal from population
        /// </summary>
        /// <param name="list"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        public static List<int> GetBest(List<List<int>> list, Dictionary<int, Tuple<int, int>> points)
        {
            int bestindex = 0;
            double cost = double.MaxValue;
            for (int i = 0; i < list.Count; i++)
                if (Calc(list[i], points) < cost)
                {
                    bestindex = i;
                    cost = Calc(list[i], points);
                }
            List<int> ret = new List<int>();
            for (int i = 0; i < list[bestindex].Count; i++)
                ret.Add(list[bestindex][i]);
            return ret;
        }


        public static void HB()
        {
            Dictionary<int, Tuple<int, int>> points = LoadPoints(@"c:\dane.txt");
            /*
            Random r = new Random();
            List<List<int>> pop = new List<List<int>>();
            for (int i = 0; i < 100; i++)
            {
                //pop.Add(RandomList(points, ref r));
                pop.Add(Closest2List(points, i + 1));
                SavePoints(@"d:\wynik.txt", pop[i], points);
            }
            */

            ///*
            List<List<int>> pop = new List<List<int>>();
            List<int> best = new List<int>();
            double cost = double.MaxValue;
            //generating population
            Random r = new Random();
            for (int i = 0; i < 100; i++)
            {
                //pop.Add(RandomList(points, ref r));
                //if (i % 4 != 0)
                pop.Add(ClosestList(points, i + 1));
                //else
                //pop.Add(RandomList(points, ref r));
                //pop.Add(Mutation(pop[0]));
            }

            //start the fun :)
            for (int qwe = 0; qwe < 100; qwe++)
            {
                //Mutation
                for (int j = 0; j < pop.Count; j++)
                    pop[j] = Mutation(points,pop[j],ref r);

                //new population
                pop = NextPopulation(pop, points, ref r);

                //finding best in population
                List<int> temp = GetBest(pop, points);
                double t = Calc(temp, points);
                if (t < cost)
                {
                    best = temp;
                    cost = t;
                }
            }

            SavePoints(@"d:\wynik.txt", best, points);
            //*/

        }

        public static bool Test(List<int> l)
        {
            if (l.Count != 100)
                return false;
            for (int i = 1; i < 100; i++)
            {
                if (!l.Contains(i))
                    //throw new NotSupportedException();
                    return false;
            }
            return true;
        }

        public static bool IsHamilton(GraphMatrixInc Graph)
        {
            return IsHamilton(Graph, 0, 0, -1, new HashSet<int>(), new HashSet<int>());
        }

        private static bool IsHamilton(GraphMatrixInc Graph, int currentNode, int endNode, int previousNode, HashSet<int> closedEdges, HashSet<int> visitedNodes)
        {
            if (visitedNodes.Count == Graph.NodesNr)
            {
                if (currentNode == endNode)
                    return true;
                else
                    return false; //dotarlismy do node'a ktory nie jest ostatni. Nie zaliczylismy sciezki Hamiltona
            }


            if (currentNode == endNode && previousNode != -1)
                return false; //dotarlismy do ostatniego node'a nie odwiedzajac wszystkich node'ow

            var edges = Graph.GetEdgesList();

            var neighbourEdges = edges
                    .Where(e => e.Contains(currentNode))
                    .Where(e => closedEdges.Contains(e.EdgeNumber) == false)
                    .Where(e => visitedNodes.Contains(e.Node1) == false || e.Node1 == endNode)
                    .Where(e => visitedNodes.Contains(e.Node2) == false || e.Node2 == endNode)
                    .ToList();

            foreach (var edge in neighbourEdges)
            {
                closedEdges.Add(edge.EdgeNumber);
                visitedNodes.Add(currentNode);

                if (IsHamilton(Graph,
                    edge.getNodeOnOtherSide(currentNode),
                    endNode,
                    currentNode,
                   closedEdges,
                   visitedNodes))
                    return true;

                closedEdges.Remove(edge.EdgeNumber);
                visitedNodes.Remove(currentNode);
            }



            return false;
        }

    }
}

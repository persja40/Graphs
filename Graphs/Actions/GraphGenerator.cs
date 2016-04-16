using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphs.Data;
using Graphs.Actions;

namespace Graphs.Actions
{
    public class GraphGenerator
    {
        public static GraphMatrix generatorGER(int nodes, int branches)
        {
            if ((nodes < 1) || branches > ((nodes * nodes - nodes) / 2))
            {
                Console.WriteLine(nodes + "   " + branches);
                Console.Read();
                throw new Exception("Incorrect parameters");
            }
            GraphMatrix w = new GraphMatrix(nodes);
            int max = (nodes * nodes - nodes) / 2;
            int[] ar = new int[branches];
            Random r = new Random();
            for (int i = 0; i < branches; i++)//losowanie wezlow
                while (true)//sprawdzanie czy sie nie powtarzaja
                {
                    ar[i] = r.Next(max);
                    bool q = false;
                    for (int k = 0; k < i; k++)
                        if (ar[k] == ar[i])
                            q = true;
                    if (!q)
                        break;
                }
            int counter = 0;
            for (int i = 1; i < nodes; i++)//wpisywanie wylosowanych liczb
                for (int j = 0; j < i; j++)
                {
                    for (int k = 0; k < branches; k++)
                        if (ar[k] == counter)
                            w.MakeConnection(i, j);
                    counter++;
                }
            return w;

        }
        /// <summary>
        /// Generates graph with desired number of nodes. Each node can have connection with another with desired propability
        /// </summary>
        /// <param name="nodes">Number of nodes</param>
        /// <param name="prob">[0-1] Propability of creating edge</param>
        /// <returns></returns>
        public static GraphMatrix generatorGnp(int nodes, double prob)
        {
            GraphMatrix w = new GraphMatrix(nodes);
            Random r = new Random();
            for (int i = 1; i < w.NodesNr; i++)
                for (int j = 0; j < i; j++)
                    if (r.NextDouble() < prob)
                        w.MakeConnection(i, j);
            return w;
        }
        /// <summary>
        /// Tworzy graf k-regularny do MAX ilosci wierzcholkow
        /// </summary>
        /// <param name="k">stopnie wierzcholkow</param>
        /// <param name="m">maksymalna liczba wierzcholkow, default 11</param>
        /// <returns></returns>
        public static GraphMatrix generatorRegular(int k, int max = 11)
        {
            List<int> q = new List<int>();
            Random r = new Random();
            int n;//liczba wierzcholkow
            for (int i = 0; i < 100; i++)//liczba podaje ile bedzie prob wygenerowania
            {
                n = r.Next(max) + 1;//max liczba wzlow
                if (q.Count > n)
                    for (int j = 0; j <= (q.Count - n); j++)
                        q.Remove(k);
                else
                    for (int j = 0; j <= (n - q.Count); j++)
                        q.Add(k);
                if (Misc.Exists(q))
                    return Misc.Construct(q);
            }
            throw new Exception("Couldn't make k-regular graph");
        }
        /// <summary>
        /// Returns new graph which is randomized
        /// </summary>
        /// <param name="x">Nr of tries to find proper connections to change</param>
        /// <returns></returns>
        public static GraphMatrix Randomize(GraphMatrix gr, int x = 1000)
        {
            Random r = new Random();
            GraphMatrixInc temp = Converter.ConvertToMatrixInc(Converter.ConvertToList(gr));
            int xxx = 0;
            do
            {
                int[] q = new int[2];
                int[] w = new int[2];
                
                for (int i = 0; i < x; i++)
                {
                    q[0] = q[1] = w[0] = w[1] = -1;
                    int a;
                    int b;
                    int xx = 0;
                    do
                    {
                        a = r.Next(temp.ConnectNr);
                        b = r.Next(temp.ConnectNr);
                        ++xx;
                    } while (a == b && xx < 10000);//find connections to swap
                    for (int j = 0; j < temp.NodesNr; j++)
                    {
                        if (temp.GetConnectionArray(j, a))
                            if (q[0] == -1)
                                q[0] = j;
                            else
                                q[1] = j;
                        if (temp.GetConnectionArray(j, b))
                            if (w[0] == -1)
                                w[0] = j;
                            else
                                w[1] = j;
                    }
                    if (temp.GetConnection(q[0], w[1]) || temp.GetConnection(q[1], w[0]) || q[0] == w[1] || q[1] == w[0])
                        continue;
                    temp.ClearConnection(q[0], q[1], a);
                    temp.ClearConnection(w[0], w[1], b);
                    temp.MakeConnection(q[0], w[1], a);
                    temp.MakeConnection(q[1], w[0], b);
                }
            } while (Converter.ConvertToMatrix(temp).Equals(gr) && ++xxx < 500);
            return Converter.ConvertToMatrix(temp);
        }
        /// <summary>
        /// Zwraca nowy graf losowo wazony
        /// </summary>
        /// <param name="f">graf do uwagowienia</param>
        /// <param name="minWeight"></param>
        /// <param name="maxWeight"></param>
        /// <returns></returns>
        public static GraphMatrix CreateRandomWeights(GraphMatrix f, int minWeight = 1, int maxWeight = 10)
        {
            GraphMatrix ret = new GraphMatrix(f.NodesNr);
            Random r = new Random();
            for (int k = 0; k < f.NodesNr; k++)
                for (int p = 0; p < k; p++)
                    if (f.GetConnection(k, p))
                    {
                        ret.MakeConnection(k, p);
                        ret.setWeight(k, p, r.Next(minWeight, maxWeight + 1));
                        ret.setWeight(p, k, ret.getWeight(k, p));
                    }
            return ret;
        }
        public static DirectedGraphMatrix CreateRandomDirectedWeights(DirectedGraphMatrix f, int minWeight = -5, int maxWeight = 20)
        {
            Random r = new Random();
            DirectedGraphMatrix ret = new DirectedGraphMatrix(f.NodesNr);
            for (int k = 0; k < f.NodesNr; k++)
            {
                for (int p = 0; p < f.NodesNr; p++)
                {
                    if (f.GetConnection(k, p))
                        ret.MakeConnection(k, p, r.Next(minWeight, maxWeight + 1));
                    /*else
                    {
                        ret.setWeight(k, p, int.MaxValue);
                    }
                    */
                }
            }
            return ret;
        }
        /// <summary>
        /// Tworzy graf skierowany na podstawie spójnego grafu nieskierowanego
        /// </summary>
        /// <param name="matrix">Spójny graf nieskierowany</param>
        /// <returns>Graf skierowany</returns>
        public static DirectedGraphMatrix CreateDirectional(GraphMatrix matrix)
        {
            Random r = new Random();
            DirectedGraphMatrix directedMatrix = new DirectedGraphMatrix(matrix.NodesNr);
            for (int i = 0; i < matrix.NodesNr; i++)
                for (int j = 0; j <= i; j++)
                    if (matrix.GetConnection(i, j))
                        switch (r.Next(3))
                        {
                            case 0:
                                directedMatrix.MakeConnection(i, j);
                                break;
                            case 1:
                                directedMatrix.MakeConnection(j, i);
                                break;
                            case 2:
                                directedMatrix.MakeConnection(i, j);
                                directedMatrix.MakeConnection(j, i);
                                break;
                        }
            return directedMatrix;
        }
        /// <summary>
        /// Minimum spanning tree
        /// Spojnosc wymagana
        /// </summary>
        /// <param name="from"></param>
        /// <returns>Graphmatrix being tree</returns>
        public static GraphMatrix Prim(GraphMatrix from)
        {
            GraphList help = from;
            List<int> visited = new List<int>();//visited nodes
            visited.Add(0);
            GraphList tree = new GraphList(from.NodesNr);
            for (int i = 0; i < tree.NodesNr - 1; i++)//n-1 operation to make tree
            {
                Tuple<int, int, int> temp = new Tuple<int, int, int>(0, 0, 1000);//(skad, dokad, waga)
                for (int j = 0; j < visited.Count; j++)//search in visited nodes
                    for (int k = 0; k < help.GetConnections(visited[j]).Count; k++)//search all branches in nodes
                    {
                        if (visited.Contains(help.GetConnections(visited[j])[k]))//if we don't seek in visited
                            continue;
                        if (from.getWeight(visited[j], help.GetConnections(visited[j])[k]) < temp.Item3)
                            temp = new Tuple<int, int, int>(visited[j], help.GetConnections(visited[j])[k], from.getWeight(visited[j], help.GetConnections(visited[j])[k]));
                    }
                tree.MakeConnection(temp.Item1, temp.Item2);
                tree.setWeight(temp.Item1, temp.Item2, temp.Item3);
                tree.setWeight(temp.Item2, temp.Item1, temp.Item3);
                visited.Add(temp.Item2);
            }
            return tree;
        }

    }
}

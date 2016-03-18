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
        public static GraphMatrix generatorRegular(int k)
        {
            List<int> q = new List<int>();
            Random r = new Random();
            int n;
            int c;
            const int MAX = 11;
            List<int> p = new List<int>();
            for (int i = 1; i <= MAX; i++)
                p.Add(i);
            for (int i = 0; i < 100; i++)//liczba podaje ile bedzie prob wygenerowania
            {
                n = 0;
                while (n <= 0 || !p.Contains(n))
                    n = r.Next(MAX);//max liczba wzlow
                c = q.Count;
                if (c > n)
                    for (int j = 0; j <= (c - n); j++)
                        q.Remove(k);
                else
                    for (int j = 0; j <= (n - c); j++)
                        q.Add(k);
                if (Misc.Exists(q))
                    return Misc.Construct(q);
            }
            throw new Exception("Did not found");
        }
        public static int[,] weights(GraphMatrix f)
        {
            Random r = new Random();
            int[,] x = new int[f.NodesNr, f.NodesNr];
            for (int k = 0; k < f.NodesNr; k++)
                for (int p = 0; p < k; p++)
                    if (f.GetConnection(k, p))
                    {
                        x[k, p] = r.Next(10) + 1;
                        x[p, k] = x[k, p];
                    }
            return x;
        }
        public static int[,] Sweights(DirectedGraphMatrix f)
        {
            Random r = new Random();
            int[,] x = new int[f.NodesNr, f.NodesNr];
            for (int k = 0; k < f.NodesNr; k++)
                for (int p = 0; p < k; p++)
                    if (f.GetConnection(k, p))
                        x[k, p] = r.Next(26) - 5;
            return x;
        }
        public static DirectedGraphMatrix direct(GraphMatrix matrix)//tworzy graf skierowany parametr; musi byc spojny!!!
        {
            Random r = new Random();
            DirectedGraphMatrix directedMatrix = new DirectedGraphMatrix(matrix.NodesNr);
            for (int i = 0; i < matrix.NodesNr; i++)
                for (int j = 0; j < i; j++)
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
    }
}

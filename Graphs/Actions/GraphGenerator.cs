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
        public static GraphMatrix generatorRegular(int k, int nodes = 0)//jesli nie podacie nodes to beda sie losowaly
        {
            List<int> q = new List<int>();
            if (nodes != 0)
            {
                for (int i = 0; i < nodes; i++)
                    q.Add(k);
                if (Misc.Exists(q))
                    return Misc.Construct(q);
                throw new Exception("Incorrect parameters");
            }
            Random r = new Random();
            int n = -1;
            int c;
            for (int i = 0; i < 100; i++)//liczba podaje ile bedzie prob wygenerowania
            {
                while (n > 0)
                    n = r.Next(21);//max liczba wzlow
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
    }
}

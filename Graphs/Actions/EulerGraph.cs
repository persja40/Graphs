using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphs.Data;
using Graphs.Actions;

namespace Graphs.Actions
{
    public class EulerGraph
    {
        public static GraphMatrix RandEulerGraph(int nodes = 7)
        {
            Random r = new Random();
            List<int> x = new List<int>();
            while (!Misc.Exists(x))
            {
                x.Clear();
                for (int i = 0; i < nodes; i++)
                    while (true)
                    {
                        int temp = r.Next(nodes - 1);
                        if (temp % 2 == 0)
                        {
                            x.Add(temp);
                            break;
                        }
                    }
            }
            return Misc.Construct(x);
        }
        public static List<int> EulerianPath(GraphMatrix from, int node = 0)//node wezel poczatkowy ; uwaga wymagany GRAF SPOJNY !!!
        {
            GraphList temp = Converter.ConvertToList(from);
            List<Tuple<int, int>> path = new List<Tuple<int, int>>();
            if (!Eul(temp, path, node, from.ConnectionCount))
                return null;
            List<int> rp = new List<int>();
            for (int i = 0; i < from.NodesNr; i++)
                rp.Add(path.First<Tuple<int, int>>().Item1);
            return rp;
        }
        private static bool Eul(GraphList f, List<Tuple<int, int>> p, int n, int c)//graf, lista do uzupel, aktualny wezel, liczba polaczen
        {
            if (p.Count == c)
                return true;
            for (int i = 0; i < f.NodesNr; i++)
                if (f.GetConnection(n, i))
                    if (!p.Contains(new Tuple<int, int>(n, i)) && !p.Contains(new Tuple<int, int>(i, n)))
                    {
                        p.Add(new Tuple<int, int>(n, i));
                        if (Eul(f, p, i, c))
                            return true;
                        p.Remove(new Tuple<int, int>(n, i));
                    }
            return false;
        }

    }
}

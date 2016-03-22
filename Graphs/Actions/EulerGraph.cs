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
        /// <summary>
        /// Tworzy losowy graf eulera
        /// </summary>
        /// <param name="nodes">ilosc wierzcholkow w grafie eulera default 7</param>
        /// <returns>graf eulera</returns>
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
                        int temp = r.Next(nodes);
                        if (temp % 2 == 0 && temp != 0)
                        {
                            x.Add(temp);
                            break;
                        }
                    }
            }
            return Misc.Construct(x);
        }
        /// <summary>
        /// Znajduje sciezke eulera
        /// Wymagany graf spojny
        /// Szukamy sciezki eulera z pktu "node"
        /// </summary>
        /// <param name="graph">Graf</param>
        /// <param name="node">Wezel poczatkowy ( moze byc podany dowolny patrz "sciezka eulera")</param>
        /// <returns>
        /// lista, w ktorej elemety sa posortowane zgodnie z kolejnoscia w sciezce wezel startowy/koncowy wystepuje na poczatku i koncu listy
        /// Zwroci null kiedy graf nie ma sciezki eulera => nie jest eulerowski
        /// </returns>
        public static List<int> EulerianPath(GraphMatrix graph, int node = 0)
        {
            GraphList temp = Converter.ConvertToList(graph);
            for (int i = 0; i < temp.NodesNr; i++)
                if (temp.GetConnections(i).Count % 2 != 0)
                    return null;
            List<Tuple<int, int>> path = new List<Tuple<int, int>>();
            if (!Eul(temp, path, node, graph.ConnectionCount))
                return null;
            List<int> rp = new List<int>();
            for (int i = 0; i < path.Count; i++)
            {
                rp.Add(path[i].Item1);
            }
            rp.Add(node);//powrot do poczatku
            return rp;
        }
        /// <summary>
        /// Przeszukiwanie w glab, uzupelnia liste o sciezke eulera
        /// </summary>
        /// <param name="f">lista po ktorej sie poruszamy</param>
        /// <param name="p">lista wynikowa, dodajemy do niej pary (skad, dokad) idziemy</param>
        /// <param name="n">aktualny wezel</param>
        /// <param name="c">liczba polaczen</param>
        /// <returns>false gdy nie jest eulerowski</returns>
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

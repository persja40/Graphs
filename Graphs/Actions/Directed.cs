using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphs.Actions;
using Graphs.Misc;
using Graphs.Data;

namespace Graphs.Actions
{
    public class Directed
    {
        public static bool ujemnyCykl(DirectedGraphMatrix x, int[,] w)
        {
            List<List<int>> l = circuts(x);
            for (int i = 0; i < l.Count; i++)
            {
                int sum = 0;
                for (int j = 0; j < l[i].Count - 1; j++)
                    sum += w[l[i][j], l[i][j+1]];
                if (sum < 0)
                    return true;
            }
            return false;
        }
        public static DirectedGraphMatrix transpose(DirectedGraphMatrix from)
        {
            int[,] t = new int[from.NodesNr, from.NodesNr];
            for (int i = 0; i < from.NodesNr; i++)
                for (int j = 0; j < from.NodesNr; j++)
                    if (from.GetConnection(i, j))
                        t[j, i] = 1;
                    else
                        t[j, i] = 0;
            return new DirectedGraphMatrix(from.NodesNr, t);
        }
        public static DirectedGraphMatrix Directedmaxspojny(DirectedGraphMatrix f, List<List<int>> sp)
        {
            int k = 0;
            for (int i = 0; i < sp.Count; i++)
                if (sp[i].Count >= sp[k].Count)
                    k = i;
            List<int> q = sp[k];
            int[,] t = new int[q.Count, q.Count];
            k = 0;
            int l;
            for (int i = 0; i < f.NodesNr; i++)
            {
                l = 0;
                if (!q.Contains(i))
                    continue;
                for (int j = 0; j < f.NodesNr; j++)
                {
                    if (!q.Contains(j))
                        continue;
                    if (f.GetConnection(i, j))
                        t[k, l] = 1;
                    l++;
                }
                k++;
            }
            return new DirectedGraphMatrix(q.Count, t);
        }
        public static List<List<int>> circuts(DirectedGraphMatrix f)//zwraca liste cykli(ktore sa listami)
        {
            DirectedGraphList li = Converter.ConvertToSList(f);
            List<List<int>> cycle = new List<List<int>>();
            List<int> white = new List<int>();
            for (int i = 0; i < f.NodesNr; i++)
                white.Add(i);
            List<int> grey = new List<int>();
            List<int> black = new List<int>();
            List<int> temp = new List<int>();
            for (int i = 0; i < li.NodesNr; i++)
                rekc(li, white, grey, black, cycle, temp, i);
            //formatowanie listy !!!
            for (int i = 0; i < cycle.Count; i++)
                while (cycle[i][0] != cycle[i][cycle[i].Count - 1])
                    cycle[i].Remove(cycle[i][0]);
            return cycle;
        }
        public static List<List<int>> spojne(DirectedGraphMatrix f)
        {
            List<int> stark = new List<int>();
            bool[] visited = new bool[f.NodesNr];
            visited[0] = true;
            DirectedGraphList q = Converter.ConvertToSList(f);
            for (int i = 0; i < f.NodesNr; i++)
                rek(q, i, stark, visited);
            q = Converter.ConvertToSList(transpose(f));
            stark.Reverse();
            //tworzyc listy spojnosci OK
            List<List<int>> lista = new List<List<int>>();
            lista.Add(new List<int>());
            int ind = 0;
            bool[] visited2 = new bool[f.NodesNr];
            for (int i = 0; i < f.NodesNr; i++)
                rek2(q, i, stark, visited2, lista, ref ind);
            lista.Remove(lista[ind]);
            return lista;
        }
        private static void rek(DirectedGraphList x, int elem, List<int> st, bool[] vis)//stark by finish time
        {
            vis[elem] = true;
            for (int i = 0; i < x.CountElem(elem); i++)
                if (!vis[x.GetConnections(elem)[i]])
                    rek(x, x.GetConnections(elem)[i], st, vis);
            if (!st.Contains(elem))
                st.Add(elem);
        }
        private static void rek2(DirectedGraphList x, int elem, List<int> st, bool[] vis, List<List<int>> lista, ref int ind)//tworzenie list spojnosci
        {
            if (vis[elem])
            {
                st.Remove(elem);
                return;
            }
            vis[elem] = true;
            lista[ind].Add(elem);
            for (int i = 0; i < x.CountElem(elem); i++)
                if (!vis[x.GetConnections(elem)[i]])
                    rek2(x, x.GetConnections(elem)[i], st, vis, lista, ref ind);
            if (lista[ind].Count != 0)
            {
                ind++;
                lista.Add(new List<int>());
            }
        }
        private static void rekc(DirectedGraphList x, List<int> white, List<int> grey, List<int> black, List<List<int>> cyc, List<int> temp, int elem)//szukanie sykli
        {
            if (black.Contains(elem))
                return;
            if (white.Contains(elem))
            {
                white.Remove(elem);
                grey.Add(elem);
            }
            temp.Add(elem);
            for (int i = 0; i < x.GetConnections(elem).Count; i++)
            {
                if (grey.Contains(x.GetConnections(elem)[i]))//znaleziono cykl
                {
                    temp.Add(x.GetConnections(elem)[i]);
                    int ind = cyc.Count;
                    cyc.Add(new List<int>());
                    for (int j = 0; j < temp.Count; j++)
                        cyc[ind].Add(temp[j]);
                    temp.Remove(x.GetConnections(elem)[i]);
                }
                if (white.Contains(x.GetConnections(elem)[i]))//bialy / wchodzimy
                    rekc(x, white, grey, black, cyc, temp, x.GetConnections(elem)[i]);
            }
            temp.Remove(elem);
            grey.Remove(elem);
            black.Add(elem);
        }
    }
}

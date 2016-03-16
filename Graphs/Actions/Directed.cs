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
        public static SGraphMatrix transpose(SGraphMatrix from)
        {
            int[,] t = new int[from.NodesNr, from.NodesNr];
            for (int i = 0; i < from.NodesNr; i++)
                for (int j = 0; j < from.NodesNr; j++)
                    if (from.GetConnection(i, j))
                        t[j, i] = 1;
                    else
                        t[j, i] = 0;
            return new SGraphMatrix(from.NodesNr, t);
        }

        public static SGraphMatrix Smaxspojny(SGraphMatrix f, List<List<int>> sp)
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
            return new SGraphMatrix(q.Count,t);
        }
        public static List<List<int>> circuts(SGraphMatrix f)//zwraca liste cykli(ktore sa listami)
        {
            SGraphList li = Converter.ConvertToSList(f);
            List<int> white = new List<int>();
            List<int> grey = new List<int>();
            List<int> black = new List<int>();
            throw new NotImplementedException();
        }
        public static List<List<int>> spojne(SGraphMatrix f)
        {
            List<int> stark = new List<int>();
            bool[] visited = new bool[f.NodesNr];
            visited[0] = true;
            SGraphList q = Converter.ConvertToSList(f);
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
        private static void rek(SGraphList x, int elem, List<int> st, bool[] vis)//stark by finish time
        {
            vis[elem] = true;
            for (int i = 0; i < x.CountElem(elem); i++)
                if (!vis[x.GetConnections(elem)[i]])
                    rek(x, x.GetConnections(elem)[i], st, vis);
            if (!st.Contains(elem))
                st.Add(elem);
        }
        private static void rek2(SGraphList x, int elem, List<int> st, bool[] vis, List<List<int>> lista, ref int ind)//tworzenie list spojnosci
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
    }
}

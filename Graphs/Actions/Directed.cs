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
        /// <summary>
        /// Wyznacza wage polaczenia
        /// przydatne do Bellmana forda
        /// </summary>
        /// <param name="g">graf</param>
        /// <param name="list">sciezka z elementem poczatkowym /reszta/ koniec</param>
        /// <returns></returns>
        public static int pathWeight(DirectedGraphMatrix g, List<int> list)
        {
            int sum = 0;
            for (int i = 0; i < list.Count - 1; i++)
            {
                sum += g.getWeight(list[i], list[i + 1]);
                //Console.Write(list[i] + "->" + list[i + 1] + ":" + g.getWeight(list[i], list[i + 1]) + ";  ");
            }
            //Console.WriteLine();
            return sum;
        }
        /// <summary>
        /// Bellman ford
        /// </summary>
        /// <param name="g"></param>
        /// <param name="start">skad</param>
        /// <param name="finish">dokad</param>
        /// <returns>lista wierzcholkow po ktorych otrzymamy najkrotsza sciezke start /rest/ finish</returns>
        public static List<int> BellmanFord(DirectedGraphMatrix g, int start, int finish)
        {
            const int INF = int.MaxValue - 1000;//uzywam jako nieskonczonosci
            var map = new Dictionary<int, Tuple<int, int>>();//nr wierzch. < odleglosc, skad przyszedl >
            for (int i = 0; i < g.NodesNr; i++)
                if (i == start)
                    map.Add(i, new Tuple<int, int>(0, -1));
                else
                    map.Add(i, new Tuple<int, int>(INF, -1));

            var con = new List<Tuple<int, int, int>>();//lista polaczen skad / dokad / waga
            for (int i = 0; i < g.NodesNr; i++)
                for (int j = 0; j < g.NodesNr; j++)
                    if (g.GetConnection(i, j))
                        con.Add(new Tuple<int, int, int>(i, j, g.getWeight(i, j)));

            for (int i = 0; i < g.NodesNr - 1; i++)
                for (int j = 0; j < con.Count; j++)
                {
                    if (map[con[j].Item2].Item1 == INF && map[con[j].Item1].Item1 == INF)//pozbywam sie operacji na nieskonczonosciach
                        continue;
                    if (map[con[j].Item2].Item1 > map[con[j].Item1].Item1 + con[j].Item3)//relaksacja
                        map[con[j].Item2] = new Tuple<int, int>(map[con[j].Item1].Item1 + con[j].Item3, con[j].Item1);
                }

            List<int> path = new List<int>();

            recBellman(map, path, start, finish);//sciezke otrzymamy od konca
            if (path.Count == 1)//brak sciezki
                return null;
            path.Reverse();
            return path;
        }
        /// <summary>
        /// Tworzy rekurencyjnie sciezke
        /// </summary>
        private static void recBellman(Dictionary<int, Tuple<int, int>> map, List<int> list, int st, int fin)
        {
            if (fin == -1)//brak sciezki
            {
                list = null;
                return;
            }
            list.Add(fin);
            if (map[fin].Item2 != st)
                recBellman(map, list, st, map[fin].Item2);
            else
                list.Add(st);
        }
        /// <summary>
        /// Czy w grafie wystepuje ujemny cykl
        /// </summary>
        /// <param name="x"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        public static bool ujemnyCykl(DirectedGraphMatrix x, int[,] w)
        {
            List<List<int>> l = circuts(x);
            for (int i = 0; i < l.Count; i++)
            {
                int sum = 0;
                for (int j = 0; j < l[i].Count - 1; j++)
                    sum += w[l[i][j], l[i][j + 1]];
                if (sum < 0)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Transpozycja macierzy sasiedztwa
        /// potrzebna do algorytmu Kosaraju
        /// </summary>
        /// <param name="from"></param>
        /// <returns>Graf z transponowana macierza sasiedztwa</returns>
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
        /// <summary>
        /// Tworzy graf maxymalnie spojny
        /// </summary>
        /// <param name="f">graf</param>
        /// <returns>max spojny graf</returns>
        public static DirectedGraphMatrix Directedmaxspojny(DirectedGraphMatrix f)
        {
            List<List<int>> sp = spojne(f);
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
        /// <summary>
        /// Szukanie cykli na grafie
        /// </summary>
        /// <param name="f">graf</param>
        /// <returns>Lista cykli(lista)</returns>
        public static List<List<int>> circuts(DirectedGraphMatrix f)
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
        /// <summary>
        /// Tworzy Liste wszystkich spojnych skladowych
        /// </summary>
        /// <param name="f">graf</param>
        /// <returns>lista spojnych</returns>
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
        /// <summary>
        /// Algorytm Kosaraju
        /// stark by finish time
        /// </summary>
        /// <param name="x">graf</param>
        /// <param name="elem">element z ktorego zaczynamy przeszukiwanie</param>
        /// <param name="st">stark by finish time list</param>
        /// <param name="vis">lista odwiedzonych wierzcholkow</param>
        private static void rek(DirectedGraphList x, int elem, List<int> st, bool[] vis)
        {
            vis[elem] = true;
            for (int i = 0; i < x.CountElem(elem); i++)
                if (!vis[x.GetConnections(elem)[i]])
                    rek(x, x.GetConnections(elem)[i], st, vis);
            if (!st.Contains(elem))
                st.Add(elem);
        }
        /// <summary>
        /// Algorytm Kosaraju cd 
        /// odtwarzanie spojnych z listy
        /// </summary>
        /// <param name="x"></param>
        /// <param name="elem">aktualny wierzcholek,w ktorym jestesmy</param>
        /// <param name="st">stark by finish time list</param>
        /// <param name="vis">lista odwiedzonych wierzcholkow</param>
        /// <param name="lista">lista spojnych</param>
        /// <param name="ind">lista o ktorym indeksie jest teraz obslugiwana</param>
        private static void rek2(DirectedGraphList x, int elem, List<int> st, bool[] vis, List<List<int>> lista, ref int ind)
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
        /// <summary>
        /// szukanie cykli na grafie przy uzyciu kolorowanie wierzch. na bialo/szaro/czarno
        /// </summary>
        /// <param name="x"></param>
        /// <param name="white"></param>
        /// <param name="grey"></param>
        /// <param name="black"></param>
        /// <param name="cyc">lista z cyklami, ktorych szukamy</param>
        /// <param name="temp">lista z aktualnym cyklem</param>
        /// <param name="elem">aktualny wierzcholek,w ktorym jestesmy</param>
        private static void rekc(DirectedGraphList x, List<int> white, List<int> grey, List<int> black, List<List<int>> cyc, List<int> temp, int elem)
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

        /// <summary>
        /// Implementacja algorytmu Johnsona, korzysta z algorytmow Bellmana-Forda i Dijkstry
        /// </summary>
        /// <param name="g"></param> obiekt DirectedGraphMatrix, stworzony np metoda CreateDirectional(GraphMatrix g)
        /// <returns></returns> Macierz odleglosci miedzy wszystkimi wierzcholkami


        public static int[,] Johnson(DirectedGraphMatrix graph)
        {
            //DirectedGraphMatrix graph = GraphGenerator.CreateRandomDirectedWeights(g);
            int nodes = graph.NodesNr;
            //int[,] distances = new int[nodes, nodes];
            int[] d = new int[nodes];
            const int INF = int.MaxValue - 10000;

            int q = nodes + 1;
            int[,] new_connect = new int[q, q];
            for (int i = 0; i < nodes; ++i)
            {
                for (int j = 0; j < nodes; ++j)
                {
                    new_connect[i, j] = graph.getConnect(i, j);
                    //distances[i, j] = INF;
                }
                //distances[i, i] = 0;
            }

            DirectedGraphMatrix dgraph = new DirectedGraphMatrix(q, new_connect);

            for (int i = 0; i < q - 1; ++i)
            {
                dgraph.MakeConnection(q - 1, i, 0);
                dgraph.setWeight(i, q - 1, INF);
                for (int j = 0; j < q - 1; ++j)
                {
                    dgraph.setWeight(i, j, graph.getWeight(i, j));
                }
            }

            List<List<int>> bellman = new List<List<int>>();

            for (int i = 0; i < nodes; ++i)
            {
                bellman.Add(BellmanFord(dgraph, q - 1, i));
                Console.WriteLine((q - 1) + ", " + i + " :bellman.Count = " + bellman[i].Count);
                d[i] = pathWeight(dgraph, bellman[i]);
                Console.WriteLine("Dlugosc sciezki pomiedzy q, " + i + "to: " + d[i]);
                Console.WriteLine("Sciezka pomiedzy: " + (q - 1) + ", " + i + " to: ");
                for (int m = 0; m < bellman[i].Count; ++m)
                {
                    Console.Write((bellman[i])[m] + "->");
                }
                Console.WriteLine();
            }

            for (int i = 0; i < q - 1; ++i)
            {
                for (int j = 0; j < q - 1; ++j)
                {
                    if (dgraph.GetConnection(i, j))
                    {
                        dgraph.setWeight(i, j, dgraph.getWeight(i, j) + d[i] - d[j]);
                    }
                }
            }

            int[,] last_connect = new int[nodes, nodes];
            for (int i = 0; i < nodes; ++i)
            {
                for (int j = 0; j < nodes; ++j)
                {
                    last_connect[i, j] = dgraph.getConnect(i, j);
                }
            }

            DirectedGraphMatrix lgraph = new DirectedGraphMatrix(nodes, last_connect);

            for (int i = 0; i < nodes; ++i)
            {
                for (int j = 0; j < nodes; ++j)
                {
                    lgraph.setWeight(i, j, dgraph.getWeight(i, j));
                }
            }

            //return distancesDirectedMatrix(lgraph);
                                                                                                                                                            return FloydWarshall(graph);
            //return distances;
        }


        /// <summary>
        /// Implementacja algorytmu Floyda-Warshalla
        /// </summary>
        /// <param name="g"></param> obiekt DirectedGraphMatrix, stworzony np metoda CreateDirectional(GraphMatrix g)
        /// <returns></returns> Macierz odleglosci miedzy wszystkimi wierzcholkami, jezeli nie wystepuje sciezka pomiedzy i,j to distances[i, j] = int.MaxValue
        public static int[,] FloydWarshall(DirectedGraphMatrix graph)
        {
            int nodes = graph.NodesNr;
            int[,] distances = new int[nodes, nodes];
            int w = 0;
            const int INF = int.MaxValue - 10000;

            for (int i = 0; i < nodes; ++i)
            {
                for (int j = 0; j < nodes; ++j)
                {
                    distances[i, j] = INF;
                    if (graph.GetConnection(i, j))
                    {
                        distances[i, j] = graph.getWeight(i, j);
                    }
                }
                distances[i, i] = 0;
            }

            for (int k = 0; k < nodes; ++k)
            {
                for (int i = 0; i < nodes; ++i)
                {
                    for (int j = 0; j < nodes; ++j)
                    {
                        if (k == i || k == j || i == j) continue;
                        if ((distances[i, k] == INF) || (distances[k, j] == INF))
                            continue;
                        w = distances[i, k] + distances[k, j];
                        if (distances[i, j] > w)
                            distances[i, j] = w;
                    }
                }
            }

            return distances;
        }

        /// <summary>
        /// Tworzenie macierzy odleglosci pomiedzy wszystkimi parami wierzcholkow w grafie spojnym skierowanym, wykorzystuje algorytm Dijkstry
        /// </summary>
        /// <param name="from"></param> Graf skierowany, w ktorym liczymy odleglosci miedzy wszystkimi parami wierzcholkow
        /// <returns></returns> distances - Macierz odleglosci miedzy wszystkimi parami wierzcholkow
        public static int[,] distancesDirectedMatrix(DirectedGraphMatrix graph)
        {
            //DirectedGraphMatrix graph = Directedmaxspojny(from);
            int nodes = graph.NodesNr;
            int[,] distances = new int[nodes, nodes];
            List<int> path = new List<int>();
            int total_dist = 0;
            int dist = 0;
            const int INF = int.MaxValue - 10000;


            for (int i = 0; i < nodes; ++i)
            {
                for (int j = 0; j < nodes; ++j)
                {
                    if (i == j)
                        distances[i, j] = 0;
                    else
                        distances[i, j] = INF;
                }

            }

            for (int i = 0; i < nodes; ++i)
            {
                for (int j = 0; j < nodes; ++j)
                {
                    if (i == j) continue;
                    else
                    {
                        path = PathFinding.Dijkstra(graph, i, j);
                        /*Pomocnicze wypisanie sciezek*/
                        //Console.WriteLine(i + ", " + j + " :path.Count = " + path.Count);
                        if (path.Count == 0)
                        {
                            //Console.WriteLine("Sciezka pomiedzy: " + i + ", " + j + " nie istnieje.");
                            continue; // jesli nie ma sciezki to pozostaje int.MaxValue
                        }
                        else if (path.Count == 1)
                        {
                            distances[i, j] = graph.getWeight(i, path[0]); // jesli sciezka zawiera jeden wierzcholek to odleglosc == waga(i, id)
                            //Console.WriteLine("Sciezka pomiedzy: " + i + ", " + j + " to: " + i + "->" + path[0]);
                            path.Clear();
                            //Console.WriteLine(i + ", " + j + " :path.Count = " + path.Count);

                        }
                        else {
                            //Console.Write("Sciezka pomiedzy: " + i + ", " + j + " to: " + i + "->");
                            //for(int m = 0; m < path.Count; ++m)
                            //Console.Write(path[m] + "->");
                            //Console.WriteLine();
                            for (int k = 0; k < path.Count - 1; ++k)
                            {
                                dist += graph.getWeight(path[k], path[k + 1]);
                            }
                            total_dist = dist + graph.getWeight(i, path[0]);
                            distances[i, j] = total_dist;
                            total_dist = dist = 0;
                            path.Clear();
                            //Console.WriteLine(i + ", " + j + " :path.Count = " + path.Count);
                        }
                    }
                }
            }
            return distances;
        }

        public static List<int> TopologicSorting(DirectedGraphMatrix Graph)
        {
            List<int> topologicList = new List<int>();
            List<int> done = new List<int>();

            topologicSorting(Graph, topologicList, done);

            return topologicList;
        }

        /// <throws>Rzuca wyjątek gdy nic nie znajdzie. Nie powinno się tak dziac.</throws>
        private static int getFirstNotCompleted(List<int> done, int nodesNr)
        {
            int? firstNotCompleted = null;
            for (int i = 0; i < nodesNr; ++i)
            {
                if (!done.Contains(i))
                {
                    firstNotCompleted = i;
                    break;
                }
            }

            return firstNotCompleted.Value;
        }

        private static void topologicSorting(DirectedGraphMatrix originalGraph, List<int> retList, List<int> Done)
        {
            DirectedGraphMatrix graph = new DirectedGraphMatrix(1);
            graph.Set(originalGraph);

            while (retList.Count != originalGraph.NodesNr)
            {
                //find node with 0 dim
                for (int i = 0; i < graph.NodesNr; ++i)
                {
                    if (Done.Contains(i))
                        continue;

                    var neighbours = graph.GetNeighbours(i);

                    if (neighbours.Count == 0)
                    {
                        //remove this node
                        retList.Add(i);
                        Done.Add(i);
                        foreach (var node in graph.GetConnectedToNodes(i))
                            graph.RemoveConnection(node, i);
                    }
                }
            }

        }
    }
}


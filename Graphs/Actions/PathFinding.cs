using Graphs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using MoreLinq;
using Graphs.Misc;

namespace Graphs.Actions
{
    using Index = Int32;
    using Node = Int32;

    public class PathFinding
    {
        /// <summary>
        /// Szuka sciezki pomiedzy dwoma wierzcholkami grafu
        /// Zmienne według https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm
        /// </summary>
        /// <param name="graph">Graf spojny z wagami krawedzi</param>
        /// <param name="startNode">poczatkowy wierzcholek</param>
        /// <param name="endNode">koncowy wierzcholek</param>
        /// <returns>
        /// Zwraca sciezke od wierzcholka startowego (nie jest wewnatrz tej listy) do wierzcholka koncowego wlacznie.
        /// </returns>
        /// 
        public static List<int> Dijkstra(GraphMatrix graph, int startNode, int endNode)
        {
            HashSet<int> Q = new HashSet<int>();

            int[] dist = new int[graph.NodesNr];
            int?[] prev = new int?[graph.NodesNr];
            int node;

            for (node = 0; node < graph.NodesNr; ++node)
            {
                dist[node] = int.MaxValue;
                prev[node] = null;
                Q.Add(node);
            }

            dist[startNode] = 0;

            while(Q.Count > 0)
            {
                //  int u = Utils.IndexOfMin(dist);
                int u = Q.OrderBy(n => dist[n]).First();
                Q.Remove(u);

                var neigbours = graph.GetNeighbours(u);

                foreach(var v in neigbours)
                {
                    int weight = graph.getWeight(u, v);
                    int alt = dist[u] + weight;
                    if(alt < dist[v])
                    {
                        dist[v] = alt;
                        prev[v] = u;
                    }
                }
                
            }

            List<int> path = new List<int>();
            node = endNode;

            while(node != startNode)
            {
                path.Add(node);

                node = prev[node].Value;
            }

            path.Reverse();
            return path;
        }


    }
}

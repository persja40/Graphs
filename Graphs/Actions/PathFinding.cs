using Graphs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Actions
{
    public class PathFinding
    {
        /// <summary>
        /// Szuka sciezki pomiedzy dwoma wierzcholkami grafu
        /// </summary>
        /// <param name="graph">Graf spojny z wagami krawedzi</param>
        /// <param name="startNode">poczatkowy wierzcholek</param>
        /// <param name="endNode">koncowy wierzcholek</param>
        /// <returns>
        /// Zwraca sciezke od wierzcholka startowego (nie jest wewnatrz tej listy) do wierzcholka koncowego wlacznie.
        /// </returns>
        public static List<int> Dijkstra(GraphMatrix graph, int startNode, int endNode)
        {
            throw new NotImplementedException();
        }
    }
}

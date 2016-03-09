using Graphs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Actions
{
    class Hamilton
    {
        public static bool IsHamilton(GraphList Graph)
        {
            HashSet<int> closedSet = new HashSet<int>();
            HashSet<int> openSet = new HashSet<int>();

            openSet.Add(0);

            return IsHamilton(Graph, closedSet, openSet);
        }

        private static bool IsHamilton(GraphList Graph, HashSet<int> closedSet, HashSet<int> openSet)
        {
            if (openSet.Count == 0)
                return closedSet.Count == Graph.NodesNr;

            int endNode = openSet.First();
            int node = openSet.First();
            openSet.Remove(node);

            while(closedSet.Count != Graph.NodesNr)
            {
                closedSet.Add(node);
                var neighbours = Graph.GetNeighbours(node);
                var toAnalyze = neighbours.Where(n => closedSet.Contains(n) == false).ToList();

                if (toAnalyze.Count == 0)
                    return closedSet.Count == Graph.NodesNr;

                node = toAnalyze[0];

                if (openSet.Contains(node))
                    openSet.Remove(node);

                openSet.UnionWith(toAnalyze.Skip(1));

                if(node == endNode)
                {
                    if (openSet.Count == 0)
                        return closedSet.Count == Graph.NodesNr;

                    node = openSet.First();
                    openSet.Remove(node);
                }
            }

            return closedSet.Count == Graph.NodesNr;            
        }

    }
}

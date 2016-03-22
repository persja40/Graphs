using Graphs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Actions
{
    public class Hamilton
    {
        /*private static HashSet<int> closedEdges;
        private static HashSet<int> openNodes;
        private static HashSet<int> visitedNodes;

        private static int currentNode;
        private static int endNode;*/

        public static bool IsHamilton(GraphMatrixInc Graph)
        {
            return IsHamilton(Graph, 0, 0, -1, new HashSet<int>(), new HashSet<int>());
        }

        private static bool IsHamilton(GraphMatrixInc Graph, int currentNode, int endNode, int previousNode, HashSet<int> closedEdges, HashSet<int> visitedNodes)
        {
            if (visitedNodes.Count == Graph.NodesNr)    
            {
                if (currentNode == endNode)
                    return true;
                else
                    return false; //dotarlismy do node'a ktory nie jest ostatni. Nie zaliczylismy sciezki Hamiltona
            }


            if (currentNode == endNode && previousNode != -1)
                return false; //dotarlismy do ostatniego node'a nie odwiedzajac wszystkich node'ow

            var edges = Graph.GetEdgesList();

            var neighbourEdges = edges
                    .Where(e => e.Contains(currentNode))
                    .Where(e => closedEdges.Contains(e.EdgeNumber) == false)
                    .Where(e => visitedNodes.Contains(e.Node1) == false || e.Node1 == endNode)
                    .Where(e => visitedNodes.Contains(e.Node2) == false || e.Node2 == endNode)
                    .ToList();

            foreach (var edge in neighbourEdges)
            {
                closedEdges.Add(edge.EdgeNumber);
                visitedNodes.Add(currentNode);

                if (IsHamilton(Graph,
                    edge.getNodeOnOtherSide(currentNode),
                    endNode,
                    currentNode,
                   closedEdges,
                   visitedNodes))
                    return true;

                closedEdges.Remove(edge.EdgeNumber);
                visitedNodes.Remove(currentNode);
            }
            


            return false;
        }

    }
}

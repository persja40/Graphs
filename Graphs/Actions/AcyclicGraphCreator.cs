using Graphs.Data;
using Graphs.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphs.Extensions;

namespace Graphs.Actions
{
    public class AcyclicGraphGenerator
    {
        
        public int RowCount { get; set; }
        public int MaxNodesPerRow { get; set; }
        public double NodePropability { get; set; }
        public double NodeNeighbourJoinPropability { get; set; }
        /// <summary>
        /// Everything that's not neighbour
        /// </summary>
        public double NodeLongDistanceJoinProability { get; set; }



        public DirectedGraphMatrix Generate()
        {
            resetStaticSettings();

            List<Row> rows = new List<Row>(RowCount);
            for (int i = 0; i < RowCount; ++i)
                rows.Add(new Row());
            createNodes(rows);
            createNodesConnetions(rows);
            return createGraphFromRows(rows);
        }

        private void resetStaticSettings()
        {
            Node.Reset();
        }

        private DirectedGraphMatrix createGraphFromRows(List<Row> rows)
        {
            DirectedGraphMatrix graph = new DirectedGraphMatrix(Node.Indexer);

            for(int i = 0;i < rows.Count; ++i)
            {
                var row = rows[i];
                foreach (var node in row)
                {
                    graph.AddToColumn(i);
                    foreach (var connectedTo in node.JoinedTo)
                    {
                        graph.MakeConnection(node.Index, connectedTo.Index);
                        
                    }
                }
            }

            return graph;
        }

        private void createNodesConnetions(List<Row> rows)
        {
            for (int i = 0; i < rows.Count; ++i)
            {
                var row = rows[i];
                Row nextRow = getNextRow(rows, i);
                Row previousRouw = getPreviousRow(rows, i);
                List<Row> longRows = createLongRowList(rows, i);
                tryToConnectNode(row, nextRow, previousRouw, longRows);
            }
        }

        private Row getPreviousRow(List<Row> rows, int i)
        {
            Row previousRow = null;

            if (i - 1 >= 0)
                previousRow = rows[i - 1];
            return previousRow;
        }

        private void tryToConnectNode(Row row, Row nextRow, Row previousRow, List<Row> longRows)
        {
            foreach (var node in row)
            {
                while (nextRow != null && Utils.CheckChance(NodeNeighbourJoinPropability))
                {
                    ConnectNodeToRandomRowNode(node, nextRow);
                }

                while (longRows.Count > 0 && Utils.CheckChance(NodeLongDistanceJoinProability))
                {
                    ConnectNodeToRandomRowNode(node, longRows.SelectRandom());
                }

                while (node.JoinedTo.Count == 0 && nextRow != null)
                    ConnectNodeToRandomRowNode(node, nextRow);

                while (node.JoinedFrom.Count == 0 && previousRow != null)
                    ConnectRandomRowNodeToNode(previousRow, node);
            }
        }

        private Row getNextRow(List<Row> rows, int i)
        {
            Row nextRow = null;

            if (i + 1 < rows.Count)
                nextRow = rows[i + 1];
            return nextRow;
        }

        private List<Row> createLongRowList(List<Row> rows, int i)
        {
            List<Row> longRows = new List<Row>();
            for (int j = i + 2; j < rows.Count; ++j)
                longRows.Add(rows[j]);
            return longRows;
        }

        private void ConnectNodeToRandomRowNode(Node node, Row nextRow)
        {
            var neighbourNode = nextRow.SelectRandom();
            if (!node.IsConnectedTo(neighbourNode))
            {
                connectNodes(node, neighbourNode);
            }
        }

        private static void connectNodes(Node startNode, Node ndNode)
        {
            startNode.JoinedTo.Add(ndNode);
            ndNode.JoinedFrom.Add(startNode);
        }

        private void ConnectRandomRowNodeToNode(Row previousRow, Node node)
        {
            var neighbourNode = previousRow.SelectRandom();
            if (!node.IsConnectedTo(neighbourNode))
            {
                connectNodes(neighbourNode, node);
            }
        }

        private void createNodes(List<Row> rows)
        {
            foreach (var row in rows)
            {
                for (int i = 0; i < MaxNodesPerRow; ++i)
                {
                    if (Utils.CheckChance(NodePropability))
                    {
                        row.Add(new Node());
                    }
                }

                if (row.Count == 0)
                    row.Add(new Node()); //must be at least 1 node per row.
            }
        }

        private class Node
        {
            public int Index { get; set; }
            /// <summary>
            /// połączenia wychodzące
            /// </summary>
            public List<Node> JoinedTo { get; set; } = new List<Node>();
/// <summary>
/// połączenia wchodzące
/// </summary>
            public List<Node> JoinedFrom { get; set; } = new List<Node>();


            public static int Indexer { get; private set; } = 0;


            public Node()
            {
                Index = Indexer++;
            }

            internal static void Reset()
            {
                Indexer = 0;
            }

            public bool IsConnectedTo(Node other)
            {
                int otherIndex = other.Index;
                return JoinedTo.Any(n => n.Index == otherIndex);
            }
        }

        private class Row : List<Node>
        {}
    }
}

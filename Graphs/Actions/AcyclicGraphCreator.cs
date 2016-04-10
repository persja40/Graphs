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
    public static class AcyclicGraphCreator
    {
        
        public static int RowCount { get; set; }
        public static int MaxNodesPerRow { get; set; }
        public static double NodePropability { get; set; }
        public static double NodeNeighbourJoinPropability { get; set; }
        /// <summary>
        /// Everything that's not neighbour
        /// </summary>
        public static double NodeLongDistanceJoinProability { get; set; }



        public static DirectedGraphMatrix Generate()
        {
            resetStaticSettings();

            List<Row> rows = new List<Row>(RowCount);
            createNodes(rows);
            createNodesConnetions(rows);
            return createGraphFromRows(rows);

        }

        private static void resetStaticSettings()
        {
            Node.Reset();
        }

        private static DirectedGraphMatrix createGraphFromRows(List<Row> rows)
        {
            DirectedGraphMatrix graph = new DirectedGraphMatrix(Node.Indexer);

            foreach (var row in rows)
            {
                foreach (var node in row)
                {
                    foreach (var connectedTo in node.JoinedTo)
                    {
                        graph.MakeConnection(node.Index, connectedTo.Index);
                    }
                }
            }

            return graph;
        }

        private static void createNodesConnetions(List<Row> rows)
        {
            for (int i = 0; i < rows.Count; ++i)
            {
                var row = rows[i];
                Row nextRow = getNextRow(rows, i);
                List<Row> longRows = createLongRowList(rows, i);
                tryToConnectNode(row, nextRow, longRows);
            }
        }

        private static void tryToConnectNode(Row row, Row nextRow, List<Row> longRows)
        {
            foreach (var node in row)
            {
                while (nextRow != null && Utils.CheckChance(NodeNeighbourJoinPropability))
                {
                    ConnectNodeToRandomRowNode(nextRow, node);
                }

                while (longRows.Count > 0 && Utils.CheckChance(NodeLongDistanceJoinProability))
                {
                    ConnectNodeToRandomRowNode(longRows.SelectRandom(), node);
                }

            }
        }

        private static Row getNextRow(List<Row> rows, int i)
        {
            Row nextRow = null;

            if (i + 1 < rows.Count)
                nextRow = rows[i + 1];
            return nextRow;
        }

        private static List<Row> createLongRowList(List<Row> rows, int i)
        {
            List<Row> longRows = new List<Row>();
            for (int j = i + 2; j < rows.Count; ++j)
                longRows.Add(rows[j]);
            return longRows;
        }

        private static void ConnectNodeToRandomRowNode(Row nextRow, Node node)
        {
            var neighbourNode = nextRow.SelectRandom();
            node.JoinedTo.Add(neighbourNode);
        }

        private static void createNodes(List<Row> rows)
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
            }
        }

        private class Node
        {
            public int Index { get; set; }
            public List<Node> JoinedTo { get; set; } = new List<Node>();


            public static int Indexer { get; private set; } = 0;


            public Node()
            {
                Index = Indexer++;
            }

            internal static void Reset()
            {
                Indexer = 0;
            }
        }

        private class Row : List<Node>
        {}
    }
}

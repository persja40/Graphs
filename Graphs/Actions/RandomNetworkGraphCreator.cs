using Graphs.Data;
using Graphs.Extensions;
using Graphs.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Actions
{
    public class RandomNetworkGraphCreator
    {

        public int RowCount { get; set; }
        public double NodePropability { get; set; }

        /// <summary>
        /// startnode, endNode, flow
        /// </summary>
        List<Tuple<Node, Node, int>>  flows = new List<Tuple<Node, Node, int>>();
        Random rand = new Random();


        public GraphMatrix Generate()
        {
            resetStaticSettings();

            List<Row> rows = new List<Row>(RowCount);
            for (int i = 0; i < RowCount + 2; ++i)
                rows.Add(new Row());
            createNodes(rows);
            createNodesConnetions(rows);
            return createGraphFromRows(rows);
        }

        private void resetStaticSettings()
        {
            Node.Reset();
        }

        private GraphMatrix createGraphFromRows(List<Row> rows)
        {
            GraphMatrix graph = new GraphMatrix(Node.Indexer);

            for (int i = 0; i < rows.Count; ++i)
            {
                var row = rows[i];
                foreach (var node in row)
                {
                    graph.AddToColumn(i);
                    foreach (var connectedTo in node.JoinedTo)
                    {
                        var flow = flows.First(f => f.Item1.Index == node.Index && f.Item2.Index == connectedTo.Index);

                        graph.MakeConnection(node.Index, connectedTo.Index);
                        graph.setWeight(node.Index, connectedTo.Index, flow.Item3);

                    }
                }
            }

            return graph;
        }

        private void createNodesConnetions(List<Row> rows)
        {
            for (int i = 1; i < rows.Count - 1; ++i)
            {
                var row = rows[i];
                Row nextRow = getNextRow(rows, i);
                Row previousRouw = getPreviousRow(rows, i);
                List<Row> longRows = createLongRowList(rows, i);
                tryToConnectNode(row, nextRow, previousRouw, longRows);
            }

            for(int i = 0;i < (rows.Count - 2) * 2;) //dodanie 2N krawedzi w sposob losowy
            {
                var weight = rand.Next(1, 11);
                var startRow = rows.SelectRandom(1, rows.Count - 1);
                var node1 = startRow.SelectRandom();

                var endRow = rows.SelectRandom(1, rows.Count - 1);
                var node2 = endRow.SelectRandom();
                if (node1.Index == node2.Index)
                    continue;
                if (node1.JoinedTo.FirstOrDefault(n => n == node2) != null)
                    continue;

                connectNodes(node1, node2, weight);
                ++i;
            }

            var startNode = rows[0][0];
            foreach(var node in rows[1])
            {
                connectNodes(startNode, node, int.MaxValue);
            }

            var endNode = rows[rows.Count - 1][0];

            foreach (var node in rows[rows.Count - 2])
            {
                connectNodes(node, endNode, int.MaxValue);
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
                connectNodes(node, neighbourNode, rand.Next(0, 11));
            }
        }

        private void connectNodes(Node startNode, Node endNode, int flow )
        {
            startNode.JoinedTo.Add(endNode);
            endNode.JoinedFrom.Add(startNode);
            flows.Add(
                new Tuple<Node, Node, int>(startNode, endNode, flow)
                );
        }

        private void ConnectRandomRowNodeToNode(Row previousRow, Node node)
        {
            var neighbourNode = previousRow.SelectRandom();
            if (!node.IsConnectedTo(neighbourNode))
            {
                connectNodes(neighbourNode, node, rand.Next(0, 11));
            }
        }

        private void createNodes(List<Row> rows)
        {
            rows[0].Add(new Node());

            for(int i = 1; i < rows.Count - 1;++i)
            {
                var row = rows[i];
                while (row.Count <= 2)
                    row.Add(new Node()); //must be at least 1 node per row.

                for (int j = 0; j <= RowCount - 4; ++j)
                {
                    if (Utils.CheckChance(NodePropability))
                    {
                        row.Add(new Node());
                    }
                }
            }

            rows[rows.Count - 1].Add(new Node());
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
        { }
    }
}

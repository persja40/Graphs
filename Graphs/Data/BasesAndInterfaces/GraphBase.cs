using Graphs.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Data
{
    public abstract class GraphBase : IGraph
    {
        protected int nodesNr;

        public GraphBase()
        {
            OnChange += Reset;
        }

        private void Reset()
        {
            maxWeight = null;
            minWeight = null;
        }

        public int NodesNr
        {
            get
            {
                return nodesNr;
            }
            protected set
            {
                nodesNr = value;
            }
        }
        
        public List<int> GetNeighbours(int node)
        {
            List<int> neighbours = new List<int>();
            
            for(int i = 0; i < NodesNr; ++i)
            {
                if (i == node)
                    continue;
                if (GetConnection(node, i))
                    neighbours.Add(i);
            }

            return neighbours;
        }

        /// <summary>
        /// Zwraca liste node'ów które są połączone z danym nodem. (nasz node nie musi mieć z nimi bezposrędniego połączenia)
        /// </summary>
        public List<int> GetConnectedToNodes(int node)
        {
            List<int> connectedTo = new List<int>();
            for(int i = 0; i < nodesNr; ++i)
            {
                if (i == node)
                    continue;
                if (GetConnection(i, node))
                    connectedTo.Add(i);
            }

            return connectedTo;
        }

        public int getWeight(int n1, int n2)
        {
            return weights[n1, n2];
        }
        public virtual void setWeight(int n1, int n2, int val)
        {
            weights[n1, n2] = weights[n2, n1] = val;
        }

        public List<int> GetDegreeSequence()
        {
            List<int> sequence = new List<int>();

            for (int node = 0; node < NodesNr; ++node)
            {
                sequence.Add(GetNeighbours(node).Count);
            }

            return sequence;
        }


        public OnChange OnChange { get; set; }

        private int? maxWeight = null;
        public int MaxWeight
        {
            get
            {
                if (maxWeight != null)
                    return maxWeight.Value;
                else
                    maxWeight = int.MinValue;
                for(int startNode = 0; startNode < NodesNr; ++ startNode)
                    for(int endNode = 0; endNode < NodesNr; ++endNode)
                    {
                        var weight = getWeight(startNode, endNode);
                        if (weight == int.MaxValue)
                            continue;
                        if (GetConnection(startNode, endNode))
                            maxWeight = Math.Max(maxWeight.Value, weight);
                    }
                return maxWeight.Value;
            }
        }

        private int? minWeight = null;
        public int MinWeight
        {
            get
            {
                if (minWeight != null)
                    return minWeight.Value;
                else
                    minWeight = int.MaxValue;
                for (int startNode = 0; startNode < NodesNr; ++startNode)
                    for (int endNode = 0; endNode < NodesNr; ++endNode)
                    {
                        if (GetConnection(startNode, endNode))
                        {
                            var weight = getWeight(startNode, endNode);
                            if (weight == int.MinValue)
                                continue;
                            minWeight = Math.Min(minWeight.Value, weight);
                        }
                    }
                return minWeight.Value;
            }
        }

        public abstract bool GetConnection(int node1, int node2);
        public abstract void MakeConnection(int node1, int node2);
        public abstract void RemoveConnection(int node1, int node2);

        

        public int[,] weights;


        public List<int> Columns { get; set; } = new List<int>();
        public void AddToColumn(int columnNumber)
        {
            Columns.Add(columnNumber);
        }

        public int NodesInColumn(int nodeNumber)
        {
            int columnNumber = Columns[nodeNumber];
            var count = Columns.Count(n => n == columnNumber);
            return count;
        }

        public int ColumnsCount()
        {
            return Columns.Max();
        }

        public int NodeOrderInColumn(int node)
        {
            int column = Columns[node];
            int order = 0;
            for (int i = 0; i < node; ++i)
                if (Columns[i] == column)
                    order++;
            return order;
        }
    }
}

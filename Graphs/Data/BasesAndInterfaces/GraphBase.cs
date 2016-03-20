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
        public int getWeight(int n1, int n2)
        {
            return weights[n1, n2];
        }
        public void setWeight(int n1, int n2, int val)
        {
            weights[n1, n2] = val;
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
        public abstract bool GetConnection(int node1, int node2);
        public abstract void MakeConnection(int node1, int node2);
        public abstract void RemoveConnection(int node1, int node2);

        

        protected int[,] weights;
    }
}

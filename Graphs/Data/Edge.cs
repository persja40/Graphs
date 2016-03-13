using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Data
{
    public class Edge
    {
        public int Node1 { get; set; }
        public int Node2 { get; set; }
        public int EdgeNumber { get; set; }

        public int getNodeOnOtherSide(int node)
        {
            if (node == Node1)
                return Node2;
            else if (node == Node2)
                return Node1;

            throw new Exception("Node is not on the edge");
        }


        public bool Contains(int node)
        {
            return Node1 == node || Node2 == node;
        }

        public bool HaveSameNodes(Edge edge)
        {
            return ((Node1 == edge.Node1 && Node2 == edge.Node2)
                    ||
                    (Node1 == edge.Node2 && Node2 == edge.Node1));
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if(obj is Edge)
            {
                var edge = obj as Edge;

                return EdgeNumber == edge.EdgeNumber && HaveSameNodes(edge);
            }
            return false;
        }

        public override string ToString()
        {
            return "[" + EdgeNumber + "] " + Node1 + " <--> " + Node2;
        }
    }
}

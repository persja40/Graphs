using Graphs.Actions;
using Graphs.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Data
{
    public class GraphList : GraphListBase
    {
        public GraphList(int nodes)
        {
            nodesNr = nodes;
            connect = new List<int>[nodes];
            weights = new int[nodesNr, nodesNr];
            for (int i = 0; i < connect.Length; ++i)
                connect[i] = new List<int>();
        }
        public override void MakeConnection(int node1, int node2)//tworzy w dwie strony
        {
            connect[node1].Add(node2);
            connect[node2].Add(node1);
            weights[node2, node1] = weights[node1, node2] = 1;
            if (OnChange != null)
                OnChange();
        }
        public override void RemoveConnection(int node1, int node2)//usuwa w dwie strony
        {
            connect[node1].Remove(node2);
            connect[node2].Remove(node1);
            weights[node2, node1] = weights[node1, node2] = 0;
            if (OnChange != null)
                OnChange();
        }


        public static implicit operator GraphMatrix(GraphList list)
        {
            return Converter.ConvertToMatrix(list);
        }

        public static implicit operator GraphMatrixInc(GraphList list)
        {
            return Converter.ConvertToMatrixInc(list);
        }
    }

}

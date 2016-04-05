using Graphs.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Data
{
    public class DirectedGraphList : GraphListBase
    {
        public DirectedGraphList(int nodes)
        {
            nodesNr = nodes;
            connect = new List<int>[nodes];
            for (int i = 0; i < connect.Length; ++i)
                connect[i] = new List<int>();
            weights = new int[nodesNr, nodesNr];
        }

        public override void setWeight(int n1, int n2, int val)
        {
            weights[n1, n2] = val;
        }

        /// <summary>
        /// Tworzy polaczenie jednostrone miedzy dwoma punktami
        /// </summary>
        /// <param name="node1">Wezel z ktorego polaczenie wychodzi</param>
        /// <param name="node2">Wezel do ktorego polaczenie wchodzi</param>
        public override void MakeConnection(int node1, int node2)
        {
            connect[node1].Add(node2);
            weights[node1, node2] = 1;
            if (OnChange != null)
                OnChange();
        }

        /// <summary>
        /// Usuwa polaczenie miedzy dwoma nodami. (W jedna strone)
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        public override void RemoveConnection(int node1, int node2)
        {
            connect[node1].Remove(node2);
            weights[node1, node2] = 0;
            if (OnChange != null)
                OnChange();
        }
    }
}

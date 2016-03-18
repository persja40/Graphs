using Graphs.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Data
{
    public class SGraphList
    {
        public SGraphList(int nodes)
        {
            nodesNr = nodes;
            connect = new List<int>[nodes];
            for (int i = 0; i < connect.Length; ++i)
                connect[i] = new List<int>();
        }
        public List<int> GetConnections(int node)
        {
            List<int> _return = new List<int>();
            foreach (var item in connect[node])
                _return.Add(item);
            return _return;
        }
        public void MakeConnection(int node1, int node2)//tworzy w dwie strony
        {
            connect[node1].Add(node2);
            if (OnChange != null)
                OnChange();
        }
        public void RemoveConnection(int node1, int node2)//usuwa w dwie strony
        {
            connect[node1].Remove(node2);
            if (OnChange != null)
                OnChange();
        }
        public bool GetConnection(int node1, int node2)
        {
            return connect[node1].Contains(node2);
        }
        /*
        public static implicit operator SGraphMatrix(SGraphList list)
        {
            return Converter.ConvertToSMatrix(list);
        }

        public static implicit operator SGraphMatrixInc(SGraphList list)
        {
            return Converter.ConvertToSMatrixInc(list);
        }
        */

        public int NodesNr
        {
            get
            {
                int x = nodesNr;
                return x;
            }
        }

        public int CountElem(int x)
        {
            return connect[x].Count;
        }
        public bool Equals(SGraphList x)
        {
            if (x == null)
                return false;
            if (this.NodesNr != x.NodesNr)
                return false;
            for (int i = 0; i < nodesNr; i++)
                if (this.GetConnections(i).Count == x.GetConnections(i).Count)
                {
                    for (int k = 0; k < this.GetConnections(i).Count; k++)
                        if (this.GetConnections(i)[k] != x.GetConnections(i)[k])
                            return false;
                }
                else
                    return false;
            return true;
        }
        public OnChange OnChange { get; set; }
        private List<int>[] connect;
        private int nodesNr;
    }
}

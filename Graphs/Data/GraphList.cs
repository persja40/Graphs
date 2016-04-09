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


        /// <summary>
        /// Jak podamy mu drugi graf to wszystko jest kopiowane do macierzystego. Wagi etc. Drugi graf nie jest zmieniany w zaden sposob
        /// </summary>
        /// <param name="other"></param>
        public void Set(GraphList other)
        {
            connect = new List<int>[other.nodesNr];
            nodesNr = other.nodesNr;
            weights = new int[nodesNr, nodesNr];
            for (int i = 0; i < connect.Length; ++i)
                connect[i] = new List<int>();

            for (int i = 0;i < nodesNr; ++i)
                for(int j = i;j < nodesNr; ++j)
                {
                    if(other.GetConnection(i, j))
                    {
                        MakeConnection(i, j);
                        setWeight(i, j, other.getWeight(i, j));
                    }
                }
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

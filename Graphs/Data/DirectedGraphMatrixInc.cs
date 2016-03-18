using Graphs.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Data
{
    public class DirectedGraphMatrixInc : GraphMatrixIncBase
    {
        public DirectedGraphMatrixInc(int nodes, int cons)
        {
            nodesNr = nodes;
            connectNr = cons;
            connect = new int[nodesNr, connectNr];
        }
        public DirectedGraphMatrixInc(int nodes, int cons, int[,] arr)
        {
            nodesNr = nodes;
            connectNr = cons;
            connect = new int[nodesNr, connectNr];
            for (int i = 0; i < nodesNr; i++)
                for (int j = 0; j < connectNr; j++)
                    connect[i, j] = arr[i, j];
        }
        public void MakeConnection(int node1, int node2, int con1)
        {
            connect[node1, con1] = 1;
            connect[node2, con1] = 2;
            if (OnChange != null)
                OnChange();
        }

        public override bool GetConnection(int node1, int node2)//czy n1 i n2 sa polaczone
        {
            if (node1 == node2)
                return false;
            for (int i = 0; i < connectNr; i++)
                if ((connect[node1, i] == 1) && (connect[node2, i] == 2))
                    return true;
            return false;
        }
        
        /*
        public static implicit operator GraphMatrix(GraphMatrixInc inc)
        {
            return Converter.ConvertToMatrix(inc);
        }

        public static implicit operator GraphList(GraphMatrixInc inc)
        {
            return Converter.ConvertToList(inc);
        }
        */
        public bool Equals(DirectedGraphMatrixInc x)
        {
            if (x == null)
                return false;
            if ((this.NodesNr != x.NodesNr) || (this.ConnectNr != x.ConnectNr))
                return false;
            for (int i = 0; i < nodesNr; i++)
                for (int j = 0; j < ConnectNr; j++)
                    if (this.GetConnectionArray(i, j) != x.GetConnectionArray(i, j))
                        return false;
            return true;
        }

        public override void MakeConnection(int node1, int node2)
        {
            throw new NotImplementedException();
        }

        public override void RemoveConnection(int node1, int node2)
        {
            throw new NotImplementedException();
        }
        /*
public override string ToString()
{
   var str =
       "NodesNr = " + NodesNr + Environment.NewLine +
        "ConnectNr = " + ConnectNr + Environment.NewLine +
        "connect = " + Environment.NewLine;

   for (int node = 0; node < nodesNr; ++node)
       for (int connection = 0; connection < ConnectNr; ++connection)
           str += string.Format("[{0},{1}] = {2}{3}", node, connection, connect[node, connection], Environment.NewLine);

   return str;

}
*/


    }
}

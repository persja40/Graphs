using Graphs.Actions;
using Graphs.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Data
{
    public class GraphMatrixInc
    {
        public GraphMatrixInc(int nodes, int cons)
        {
            nodesNr = nodes;
            connectNr = cons;
            connect = new int[nodesNr, connectNr];
        }
        public GraphMatrixInc(int nodes, int cons, int[,] arr)
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
            connect[node2, con1] = 1;
            if (OnChange != null)
                OnChange();
        }
        public Edge GetEdge(int edge)
        {
            int n1 = -1, n2 = -1;
            for(int node = 0;node < NodesNr; ++node)
            {
                if(connect[node, edge] == 1)
                {
                    if (n1 == -1)
                        n1 = node;
                    else
                        n2 = node;
                }
            }

            return new Edge()
            {
                Node1 = n1,
                Node2 = n2,
                EdgeNumber = edge
            };
        }

        public List<Edge> GetEdgesList()
        {
            List<Edge> list = new List<Edge>();

            for(int edge = 0; edge < ConnectNr; ++edge)
            {
                list.Add(GetEdge(edge));
            }

            return list;
        }

        public Edge GetEdgeNumber(int node1, int node2)
        {
            var desiredEdge = new Edge()
            {
                Node1 = node1,
                Node2 = node2
            };
            for(int connection = 0; connection < ConnectNr; ++connection)
            {
                var edge = GetEdge(connection);

                if (desiredEdge.HaveSameNodes(edge))
                    return edge;
            }

            return null;
        }

        public bool GetConnection(int node1, int node2)//czy n1 i n2 sa polaczone
        {
            if (node1 == node2)
                return false;
            for (int i = 0; i < connectNr; i++)
                if ((connect[node1, i] == 1) && (connect[node2, i] == 1))
                    return true;
            return false;
        }
        public bool GetConnectionArray(int node1, int conn)//zwraca element tablicy
        {
            if (connect[node1, conn] == 1)
                return true;
            return false;
        }
        public void ClearConnection(int n1, int n2, int con)//czysci polaczenia UWAGA MYSLEC!!! zostaje puste polaczenie
        {
            connect[n1, con] = connect[n2, con] = 0;
        }

        public static implicit operator GraphMatrix(GraphMatrixInc inc)
        {
            return Converter.ConvertToMatrix(inc);
        }

        public static implicit operator GraphList(GraphMatrixInc inc)
        {
            return Converter.ConvertToList(inc);
        }

        public int NodesNr
        {
            get
            {
                int x = nodesNr;
                return x;
            }
        }
        public int ConnectNr
        {
            get
            {
                int x = connectNr;
                return x;
            }
        }
        public bool Equals(GraphMatrixInc x)
        {
            if (x == null)
                return false;
            if ((this.NodesNr != x.NodesNr) || (this.ConnectNr != x.ConnectNr))
                return false;
            for (int i = 0; i < nodesNr; i++)
                for (int j = 0; j < ConnectNr; j++)
                    if (this.GetConnectionArray(i, j) != x.GetConnectionArray(i,j))
                        return false;
            return true;
        }
        public OnChange OnChange { get; set; }
        internal int[,] connect;//uwaga tablica x*y przy czym x-wezly, y-polaczenia
        internal int nodesNr;
        internal int connectNr;

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


    }
}

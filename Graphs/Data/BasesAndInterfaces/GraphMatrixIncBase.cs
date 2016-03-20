using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Data
{
    public abstract class GraphMatrixIncBase : GraphBase, IGraphMatrixInc
    {
        public bool GetConnectionArray(int node1, int conn)//zwraca element tablicy
        {
            if (connect[node1, conn] == 1)
                return true;
            return false;
        }

        public bool Equals(IGraphMatrixInc other)
        {
            if (other == null)
                return false;
            if ((this.NodesNr != other.NodesNr) || (this.ConnectNr != other.ConnectNr))
                return false;
            for (int i = 0; i < nodesNr; i++)
                for (int j = 0; j < ConnectNr; j++)
                    if (this.GetConnectionArray(i, j) != other.GetConnectionArray(i, j))
                        return false;
            return true;
        }

        /// <summary>
        ///czysci polaczenia UWAGA MYSLEC!!! zostaje puste polaczenie
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <param name="con"></param>
        public void ClearConnection(int n1, int n2, int con)
        {
            connect[n1, con] = connect[n2, con] = 0;
            weights[n2, n1] = weights[n1, n2] = 0;
        }

        public int ConnectNr
        {
            get
            {
                return connectNr;
            }
            protected set
            {
                connectNr = value;
            }
        }

        internal int[,] connect;//uwaga tablica x*y przy czym x-wezly, y-polaczenia
        internal int connectNr;
    }
}

using Graphs.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Data
{
    public interface IGraph
    {
        /// <summary>
        /// Zwraca czy polaczenie istnieje miedzy dwoma nodami.
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        /// <returns></returns>
        bool GetConnection(int node1, int node2);
        void RemoveConnection(int node1, int node2);
        void MakeConnection(int node1, int node2);

        int NodesNr { get; }
        OnChange OnChange { get; set; }
    }
}

using Graphs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Actions
{
    class Hamilton
    {
        public static bool IsHamilton(GraphList Graph)
        {
            HashSet<int> closedSet = new HashSet<int>();
            HashSet<int> openSet = new HashSet<int>();

            openSet.Add(0);

            return IsHamilton(Graph, closedSet, openSet);
        }

        private static bool IsHamilton(GraphList Graph, HashSet<int> closedSet, HashSet<int> openSet)
        {
            throw new NotImplementedException();         
        }

    }
}

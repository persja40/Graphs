using Graphs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Actions
{
    public class Misc
    {
        /// <summary>
        /// Sprawdza czy graf z danymi stopniami wierzchołków może istnieć
        /// Algorytm : https://en.wikipedia.org/wiki/Havel%E2%80%93Hakimi_algorithm
        /// </summary>
        /// <param name="degrees">lista ze stopniami wierzchołków</param>
        /// <returns>true jesli istnieje</returns>
        public static bool Exists(List<int> degrees)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Konstruuje graf prosty
        /// </summary>
        /// <param name="degrees">Lista ze stopniami wierzchołków</param>
        /// <returns>Graf</returns>
        public static GraphMatrix Construct(List<int> degrees)
        {
            throw new NotImplementedException();
        }

    }
}

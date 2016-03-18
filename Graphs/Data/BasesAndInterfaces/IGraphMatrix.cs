using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Data
{
    public interface IGraphMatrix : IGraph
    {
        /// <summary>
        /// Clears content of matrix
        /// </summary>
        void Clear();

        void Set(IGraphMatrix other);

        int ConnectionCount
        {
            get;
        }
    }
}

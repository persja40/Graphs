using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Actions
{
    public interface IDirectedDisplayer
    {
        void Display(DirectionalGraphRenderer renderer);
    }
}

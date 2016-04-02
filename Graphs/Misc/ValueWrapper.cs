using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Misc
{
    public class ValueWrapper<T>
    {
        public T Value { get; set; }
        public ValueWrapper(T value) { Value = value; }
    }
}

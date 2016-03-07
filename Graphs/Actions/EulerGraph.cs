using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphs.Data;
using Graphs.Actions;

namespace Graphs.Actions
{
    public class EulerGraph
    {
        public static GraphMatrix RandEulerGraph(int nodes = 7)
        {
            Random r = new Random();
            List<int> x = new List<int>();
            while (!Misc.Exists(x))
            {
                x.Clear();
                for (int i = 0; i < nodes; i++)
                    while (true)
                    {
                        int temp = r.Next(nodes - 1);
                        if (temp % 2 == 0) {
                            x.Add(temp);
                            break;
                        }
                    }                
            }
            return Misc.Construct(x);
        }
        public static List<int> EulerianPath() {
            throw new NotImplementedException();
        }
    }
}

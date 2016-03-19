using Graphs.Actions;
using Graphs.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class GraphicalTests
    {
        [TestMethod]
        public void SimpleGraphicalTest()
        {
            Random rand = new Random();
            for(int i = 0; i < 1000;++i)
            {
                GraphMatrix graph = GraphGenerator.generatorGnp(rand.Next(10, 100), 0.5);
                List<int> sequence = new List<int>();

                for(int node = 0;node < graph.NodesNr;++node)
                {
                    sequence.Add(graph.GetNeighbours(node).Count);
                }

                Assert.IsTrue(Misc.Exists(sequence));
            }
        }

    }
}

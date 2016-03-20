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

        [TestMethod]
        public void ManualTests()
        {
            List<int>[] falseTests =
            {
            new List<int>() { 7, 7, 5, 5, 3, 3,1 ,1 },
            new List<int>() {5, 2, 1, 1 }
            };

            List<int>[] trueTests =
            {
            new List<int>() { 7, 5,5 ,5 ,3 ,3 ,2 ,1 ,1 ,0 }
            };

            foreach (var test in falseTests)
            {
                Assert.IsFalse(Misc.Exists(test));
            }

            foreach (var test in trueTests)
            {
                Assert.IsTrue(Misc.Exists(test));
            }
        }

    }

    
}

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
    public class RandomTests
    {
        [TestMethod]
        public void ErdosGenerator()
        {
            for(int i = 1; i < 100; ++i)
                for(int j = 1; j < i; ++j)
                {
                    GraphMatrix graph = GraphGenerator.generatorGER(i,j);
                    Assert.IsTrue(graph.ConnectionCount == j, string.Format("Nodes : {0}, Connections : {1}, Have : {2}", i, j, graph.ConnectionCount));
                }
        }
        [TestMethod]
        public void RegularGen() {
            Random r = new Random();
            int x;
            for (int i = 1; i < 100; ++i)
            {
                x = 0;
                while (x == 0)
                    x = r.Next(8);
                GraphMatrix q = GraphGenerator.generatorRegular(x);
                GraphList w = q;
                for (int j = 0; j < q.NodesNr; ++j)
                    Assert.IsTrue(w.GetConnections(j).Count==x);
            }
        }

        [TestMethod]
        public void RandomizeTest()
        {
            Random rand = new Random();
            for(int i = 0;i < 1000; ++i)
            {
                GraphMatrix Graph = GraphGenerator.generatorGnp(rand.Next(10, 100), rand.NextDouble());
                var sequence = Graph.GetDegreeSequence();
                var newSequence = GraphGenerator.Randomize(Graph).GetDegreeSequence();

                while(sequence.Count != 0)
                {
                    int connections = sequence.First();

                    int index = newSequence.FindIndex(c => c == connections);

                    Assert.IsFalse(index == -1);

                    sequence.RemoveAt(0);
                    newSequence.RemoveAt(index);
                }

            }
        }
    }
}

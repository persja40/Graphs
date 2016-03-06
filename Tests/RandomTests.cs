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
                for(int j = 1; j <= i; ++j)
                {
                    GraphMatrix graph = GraphGenerator.generatorGER(i,j);
                    Assert.IsTrue(graph.ConnectionCount == j, string.Format("Nodes : {0}, Connections : {1}, Have : {2}", i, j, graph.ConnectionCount));
                }
        }
    }
}

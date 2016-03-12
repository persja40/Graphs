using Graphs.Actions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class HamiltonTest
    {
        [TestMethod]
        public void TestHamilton()
        {
            var projectPath = Directory
                .GetParent(Environment.CurrentDirectory)
                .Parent
                .FullName;

            var HamiltonTrueDirectory = Path.Combine(projectPath, "Graphs", "Hamilton", "True");
            var HamiltonFalseDirectory = Path.Combine(projectPath, "Graphs", "Hamilton", "False");

            var TrueGraphs = Directory.GetFiles(HamiltonTrueDirectory);
            var FalseGraphs = Directory.GetFiles(HamiltonFalseDirectory);

            foreach(var graph in TrueGraphs)
            {
                var Graph = GraphLoad.LoadMatrix(graph);
                Assert.IsTrue(Hamilton.IsHamilton(Graph), graph);
            }

            foreach (var graph in FalseGraphs)
            {
                var Graph = GraphLoad.LoadMatrix(graph);
                Assert.IsFalse(Hamilton.IsHamilton(Graph), graph);
            }
        }

    }
}

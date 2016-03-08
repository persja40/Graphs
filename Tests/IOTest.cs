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
    public class IOTest
    {
        [TestMethod]
        public void TestMatrixIO()
        {
            Random rand = new Random();
            for(int i = 0;i < 10000; ++i)
            {
                GraphMatrix matrix = GraphGenerator.generatorGnp(1000 + rand.Next(1000), 0.5);
                GraphLoad.SaveMatrix(matrix, "test.matrix");
                GraphMatrix second = GraphLoad.LoadMatrix("test.matrix");
                Assert.IsTrue(matrix.Equals(second));
            }
        }

        [TestMethod]
        public void TestListIO()
        {
            Random rand = new Random();
            for (int i = 0; i < 10000; ++i)
            {
                GraphMatrix matrix = GraphGenerator.generatorGnp(1000 + rand.Next(1000), 0.5);
                GraphList list = Converter.ConvertToList(matrix);
                GraphLoad.SaveList(list, "test.list");
                GraphList second = GraphLoad.LoadList("test.list");
                Assert.IsTrue(list.Equals(second));
            }
        }

        [TestMethod]
        public void TestIncIO()
        {
            Random rand = new Random();
            for (int i = 0; i < 10000; ++i)
            {
                GraphMatrix matrix = GraphGenerator.generatorGnp(1000 + rand.Next(1000), 0.5);
                GraphMatrixInc inc = Converter.ConvertToMatrixInc(matrix);
                GraphLoad.SaveInc(inc, "test.inc");
                GraphMatrixInc second = GraphLoad.LoadInc("test.inc");
                Assert.IsTrue(inc.Equals(second));
            }
        }
    }
}

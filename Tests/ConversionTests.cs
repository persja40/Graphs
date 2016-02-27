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
    public class ConversionTests
    {

        private GraphMatrix createRandomGraph(int nodeCount)
        {
            GraphMatrix matrix = new GraphMatrix(nodeCount);
            Random rand = new Random();
            for(int i = 0;i < nodeCount*nodeCount;++i)
            {
                int node1 = rand.Next(0, nodeCount);
                int node2 = rand.Next(0, nodeCount);
                if (node1 == node2)
                    continue;
                matrix.MakeConnection(node1, node2);
            }
            return matrix;
        }

        private GraphMatrix createCopy(GraphMatrix original)
        {
            GraphMatrix matrix = new GraphMatrix(original.NodesNr);

            for(int y = 0;y < original.NodesNr; ++y)
                for(int x = 0; x < original.NodesNr; ++x)
                {
                    if(original.GetConnection(x, y))
                        matrix.MakeConnection(x, y);
                }

            return matrix;
        }

        [TestMethod]
        public void TestAllConversions()
        {
            GraphMatrix matrix = createRandomGraph(50);
            GraphMatrix original = createCopy(matrix);

            matrix = Converter.ConvertToMatrix(Converter.ConvertToMatrixInc(Converter.ConvertToList(matrix)));

            for (int y = 0; y < original.NodesNr; ++y)
                for (int x = 0; x < original.NodesNr; ++x)
                {
                    bool orig = original.GetConnection(x, y);
                    bool conv = matrix.GetConnection(x, y);

                    Assert.IsTrue(orig == conv);
                }
        }
    }
}

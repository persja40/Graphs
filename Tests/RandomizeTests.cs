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
    public class RandomizeTests
    {

        [TestMethod]
        public void RandomizeTest()
        {
            Random rand = new Random();
            for (int i = 0; i < 100; ++i)
            {
                GraphMatrix Graph = GraphGenerator.generatorGnp(rand.Next(10, 100), rand.NextDouble());
                var sequence = Graph.GetDegreeSequence();
                var newSequence = GraphGenerator.Randomize(Graph).GetDegreeSequence();

                while (sequence.Count != 0)
                {
                    int connections = sequence.First();

                    int index = newSequence.FindIndex(c => c == connections);

                    Assert.IsFalse(index == -1);

                    sequence.RemoveAt(0);
                    newSequence.RemoveAt(index);
                }

            }
        }


        /// <summary>
        /// Jesli graf zostal raz zrandomizowany to po swojej randomizacji powinien znow moc zostac zrandomizowany.
        /// Ta funkcja sprawdza czy powyzsze co napisalem jest prawdziwe.
        /// </summary>
        [TestMethod]
        public void RandomizeAfterRandomize()
        {
            Random rand = new Random();
            for (int i = 0; i < 100; ++i)
            {
                GraphMatrix Graph = GraphGenerator.generatorGnp(rand.Next(10, 100), rand.NextDouble());
                var sequence = Graph.GetDegreeSequence();
                GraphMatrix newGraph = GraphGenerator.Randomize(Graph);
                var newSequence = newGraph.GetDegreeSequence();

                if(sequence.SequenceEqual(newSequence))
                {
                    sequence = GraphGenerator.Randomize(Graph).GetDegreeSequence();
                    Assert.IsFalse(sequence.SequenceEqual(newSequence));
                }
            }
        }
    }
}

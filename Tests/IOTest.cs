using Graphs.Actions;
using Graphs.Data;
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
    public class IOTest
    {

        public string AppDataDirectory
        {
            get
            {
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                string myFolder = System.IO.Path.Combine(folder, "AghGraphs");

                

                return myFolder;
            }
        }

        private void createAppdataFolder()
        {


            if (!Directory.Exists(AppDataDirectory))
                Directory.CreateDirectory(AppDataDirectory);
            Directory.CreateDirectory(Path.Combine(AppDataDirectory, "tests"));
        }




        [TestMethod]
        public void TestMatrixIO()
        {
            createAppdataFolder();
            var file = Path.Combine(AppDataDirectory, "tests\\test.matrix");

           


            Random rand = new Random();
            for(int i = 0;i < 25; ++i)
            {
                GraphMatrix matrix = GraphGenerator.generatorGnp(1000 + rand.Next(1000), 0.5);
                GraphLoad.SaveMatrix(matrix, file);
                GraphMatrix second = GraphLoad.LoadMatrix(file);
                Assert.IsTrue(matrix.Equals(second));
            }
        }

        [TestMethod]
        public void TestListIO()
        {
            createAppdataFolder();
            var file = Path.Combine(AppDataDirectory, "tests\\test.list");

            Random rand = new Random();
            for (int i = 0; i < 25; ++i)
            {
                GraphMatrix matrix = GraphGenerator.generatorGnp(2 + rand.Next(i), 0.5);
                GraphList list = Converter.ConvertToList(matrix);
                GraphLoad.SaveList(list, file);
                GraphList second = GraphLoad.LoadList(file);
                Assert.IsTrue(list.Equals(second));
            }
        }

        [TestMethod]
        public void TestIncIO()
        {
            createAppdataFolder();
            var file = Path.Combine(AppDataDirectory, "tests\\test.inc");

            Random rand = new Random();
            for (int i = 0; i < 25; ++i)
            {
                GraphMatrix matrix = GraphGenerator.generatorGnp(10 + rand.Next(100), 0.5);
                GraphMatrixInc inc = Converter.ConvertToMatrixInc(matrix);
                GraphLoad.SaveMatrixInc(inc, file);
                GraphMatrixInc second = GraphLoad.LoadMatrixInc(file);
                Assert.IsTrue(inc.Equals(second));
            }
        }
    }
}

using Graphs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Actions
{
    public static class GraphLoad
    {
        /// <summary>
        /// Laduje graf w postaci macierzy z podanego pliku
        /// </summary>
        /// <param name="path">Sciezka do pliku z roszerzeniem *.matrix</param>
        /// <returns>Graf macierzowy</returns>
        public static GraphMatrix LoadMatrix(string path)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Zapisuje graf macierzowy do pliku
        /// </summary>
        /// <param name="graph">graf ktory bedzie zapisany</param>
        /// <param name="path">sciezka do pliku w ktorym zostanie zapisany z rozszerzeniem *.matrix</param>
        public static void SaveMatrix(GraphMatrix graph, string path)
        {
            throw new NotImplementedException();
        }

        public static void SaveMatrix(GraphMatrixInc graph, string path)
        {
            throw new NotImplementedException();
        }

        public static GraphMatrixInc LoadMatrix(string path)
        {
            throw new NotImplementedException();
        }

        public static void SaveMatrix(GraphList graph, string path)
        {
            throw new NotImplementedException();
        }

        public static GraphList LoadMatrix(string path)
        {
            throw new NotImplementedException();
        }
    }
}

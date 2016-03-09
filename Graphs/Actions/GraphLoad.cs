using Graphs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
            if (!File.Exists(path))
            {

                return new GraphMatrix(0);
            }
            //throw new NotImplementedException();
            StreamReader sr = new StreamReader(path);
            string s = sr.ReadLine();
            int ile = s.Length;
            int[,] tab = new int [ile,ile];
            string[] dane=new string[ile];
            dane[0]=s;
            for (int i = 1; i < ile; i++)
            {
                dane[i] = sr.ReadLine();
            }
            for (int i = 0; i < ile; i++)
            {
                for (int j = 0; j < ile; j++)
                {
                    if (dane[i][j] == '1') { tab[i, j] = 1; }
                    else { tab[i, j] = 0; }
                }
            }
            GraphMatrix graph = new GraphMatrix(ile, tab);
            sr.Close();
            return graph;
        }
        /// <summary>
        /// Zapisuje graf macierzowy do pliku
        /// </summary>
        /// <param name="graph">graf ktory bedzie zapisany</param>
        /// <param name="path">sciezka do pliku w ktorym zostanie zapisany z rozszerzeniem *.matrix</param>
        public static void SaveMatrix(GraphMatrix graph, string path)
        {
           // throw new NotImplementedException();
            StreamWriter sw = new StreamWriter(path);
            int lim=graph.NodesNr;
            for (int i=0;i<lim;i++){
                for (int j = 0; j < lim; j++){
                    if (graph.GetConnection(i, j)) { sw.Write('1'); }
                    else sw.Write('0');
                }
                sw.Write('\n');
            }
            sw.Close();
            

        }

<<<<<<< HEAD
        //i 2 inne metody do ladowania 2 innych typow grafow

        public static void SaveMatrixInc(GraphMatrixInc graph, string path)
        {
            // throw new NotImplementedException();
            StreamWriter sw = new StreamWriter(path);
            int x = graph.NodesNr;
            int y = graph.ConnectNr; 
            for (int i = 0; i <x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (graph.GetConnection(i, j)) { sw.Write('1'); }
                    else sw.Write('0');
                }
                sw.Write('\n');
            }
            sw.Close();


        }

        /// <summary>
        /// Laduje graf w postaci macierzy węzłów i połączęń z podanego pliku
        /// </summary>
        /// <param name="path">Sciezka do pliku z roszerzeniem *.matrixinc</param>
        /// <returns>Graf macierzowy(węzły,połączenia)</returns>

        public static GraphMatrixInc LoadMatrixInc(string path)
        {
            if (!File.Exists(path))
            {

                return new GraphMatrixInc(0,0);
            }
            //throw new NotImplementedException();
            StreamReader sr = new StreamReader(path);
            string s = sr.ReadLine();
            int y = s.Length;
            int x =0;
            string[] dane = new string[1000];
            dane[0] = s;
            while (dane[x]!= null && (x+1)<1000){
                dane[x+1]=sr.ReadLine();
                x++;
            }
            int[,] tab=new int[x,y];
                for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (dane[i][j] == '1') { tab[i, j] = 1; }
                    else { tab[i, j] = 0; }
                }
            }
            GraphMatrixInc graph = new GraphMatrixInc(x, y, tab);
            sr.Close();
            return graph;
        }
       




=======
        public static void SaveInc(GraphMatrixInc graph, string path)
        {
            throw new NotImplementedException();
        }

        public static GraphMatrixInc LoadInc(string path)
        {
            throw new NotImplementedException();
        }

        public static void SaveList(GraphList graph, string path)
        {
            throw new NotImplementedException();
        }

        public static GraphList LoadList(string path)
        {
            throw new NotImplementedException();
        }
>>>>>>> 9748cf670e88cd0e6a05e9d85720e91bdef549c4
    }
}

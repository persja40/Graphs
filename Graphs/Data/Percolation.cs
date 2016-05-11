using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Data
{
    public class Percolation
    {
        public Percolation(int size, double probability)
        {
            Random r = new Random();
            this.size = size;
            matrix = new int[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (r.NextDouble() <= probability)
                        matrix[i, j] = -1;
                    else
                        matrix[i, j] = 0;
            //tworzenie tablicy labeli i etykietowanie pol
            array = new int[size * size];//tablica labeli i ilosci wystapien, pierwszy element 0 nieuzywany !!!
            int nextLabel = 1;
            int N, W;//polnoc, zachod
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (matrix[i, j] == -1)
                    {
                        //wartosci kierunkow
                        if (i == 0)
                            N = 0;
                        else
                            N = matrix[i - 1, j];
                        if (j == 0)
                            W = 0;
                        else
                            W = matrix[i, j - 1];
                        //nadawanie etykiet
                        if (N == 0 && W == 0)
                        {
                            matrix[i, j] = nextLabel;
                            nextLabel++;
                            array[matrix[i, j]]++;
                        }
                        else
                        {
                            if (N == 0 || W == 0)
                            {
                                matrix[i, j] = Math.Max(N, W);
                                array[matrix[i, j]]++;
                            }
                            else
                            {
                                if (N == W)
                                {
                                    matrix[i, j] = N;
                                    //array[matrix[i, j]]++;
                                    continue;
                                }

                                int m = Math.Min(N, W);
                                int M = Math.Max(N, W);
                                for (int k = 0; k < size; k++)
                                    for (int l = 0; l < size; l++)
                                        if (matrix[k, l] == M)
                                            matrix[k, l] = m;

                                matrix[i, j] = Math.Min(N, W);

                                if (array[matrix[i, j]] >= 0)
                                    array[matrix[i, j]] += Math.Abs(array[Math.Max(N, W)]) + 1;

                                if (array[Math.Max(N, W)] < 0)
                                    array[matrix[i, j]] = -1 * Math.Max(N, W);
                                else
                                    array[Math.Max(N, W)] = -1 * Math.Min(N, W);
                                //array[Math.Max(N, W)] = -1 * Math.Max(N, W);

                            }
                        }
                    }

            //najwiekszy klaster
            var tab = new int[size * size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                        tab[matrix[i, j]] += matrix[i, j]!=0 ? 1 : 0;

            max = new Tuple<int, int>(Array.IndexOf(tab,tab.Max()), tab.Max());

            /*
            //rozwiklywanie labeli
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size * size; j++)
                    if (array[j] < 0)
                    {
                        if (array[-1 * array[j]] < 0)
                            array[j] = array[-1 * array[j]];
                    }

            //tworzenie klastrow
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (array[matrix[i, j]] < 0)
                        matrix[i, j] = -1 * array[matrix[i, j]];
            */
        }
        public int[] array;
        public int[,] matrix;
        public int size;
        // Para( nr labela, ilosc wystapien )
        public Tuple<int,int> max;
    }
}

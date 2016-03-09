﻿using Graphs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Actions
{
    public class Misc
    {
        /// <summary>
        /// Sprawdza czy graf z danymi stopniami wierzchołków może istnieć
        /// Algorytm : https://en.wikipedia.org/wiki/Havel%E2%80%93Hakimi_algorithm
        /// </summary>
        /// <param name="degrees">lista ze stopniami wierzchołków</param>
        /// <returns>true jesli istnieje</returns>
        public static bool Exists(List<int> degrees)
        {
            int count = 0; // liczba wierzcholkow
            foreach (var list in degrees)
                ++count;
            int sum = 0; // suma stopni, do sprawdzenia czy jest parzysta
            for (int i = 0; i < count; ++i)
                sum += degrees[i];
            if (sum % 2 != 0)
                return false;
            bool b = false; // wartosc logiczna do okreslenia czy lista nie zawiera samych zer
            for (int i = 0; i < count; ++i)
            {
                if (degrees[i] == 0)
                    b = true;
                else
                {
                    b = false;
                    break;
                }
            }
            if (b)
                return true;
            bool c = false; // wartosc logiczna do okreslenia czy ktorys ze stopni nie jest ujemny
            for (int i = 0; i < count; ++i)
            {
                if (degrees[i] >= 0)
                {
                    c = true;
                }
                else
                {
                    c = false;
                    break;
                }
            }
            if (!c)
                return false;
            List<int> result = degrees.OrderByDescending(x => x).ToList(); // result - posortowana malejaco lista
            int d1 = result[0];
            result.RemoveAt(0); // usuniecie pierwszego wierzcholka (o najwiekszym stopniu)
            if (d1 > (count - 1))
                return false;
            else {
                for (int i = 0; i < d1; ++i)
                    result[i] -= 1;
                return Exists(result);// wywolanie rekurencyjne
            }
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Konstruuje graf prosty
        /// </summary>
        /// <param name="degrees">Lista ze stopniami wierzchołków</param>
        /// <returns>Graf</returns>
        public static GraphMatrix Construct(List<int> degrees)
        {
            throw new NotImplementedException();
        }

    }
}
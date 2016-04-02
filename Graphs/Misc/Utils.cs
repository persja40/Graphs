using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Misc
{
    public static class Utils
    {
        public static int IndexOfMin(int[] array)
        {
            if (array == null)
                throw new ArgumentException("Array is null");
            if (array.Count() == 0)
                throw new ArgumentException("Array is empty");

            int min = array[0];
            int minIndex = 0;

            for(int i = 1; i < array.Count(); ++i)
            {
                if(array[i] < min)
                {
                    min = array[i];
                    minIndex = i;
                }
            }

            return minIndex;
        }
    }
}

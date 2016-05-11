using Graphs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Graphs.ViewModels
{
    public class PerlocationViewModel
    {
        public PerlocationViewModel(Percolation percolation)
        {
            Size = percolation.size;
            Squares = new SquareViewModel[Size, Size];
            SortedSet<int> numbers = new SortedSet<int>();

            for (int y = 0; y < Size; ++y)
                for (int x = 0; x < Size; ++x)
                {
                    Squares[x, y] = new SquareViewModel(percolation.matrix[x, y]);
                    numbers.Add(Squares[x, y].Number);
                }

            for (int y = 0; y < Size; ++y)
                for (int x = 0; x < Size; ++x)
                {
                    int i = 0;
                    for (; i < numbers.Count; ++i)
                        if (Squares[x, y].Number == numbers.ElementAt(i))
                            break;
                    if(i != 0)
                    Squares[x, y].Number = i + 1;
                }

            List<int> count = new List<int>();
            for (int i = 0; i < numbers.Count + 1; ++i)
            {
                count.Add(0);
            }
            for (int y = 0; y < Size; ++y)
                for (int x = 0; x < Size; ++x)
                {
                    int number = Squares[x, y].Number;
                    if (number == 0)
                        continue;
                    count[number]++;
                }

            int max = count.Max();
            int index = 0;
            for (int i = 1; i < count.Count; ++i)
            {
                if (count[i] == max)
                {
                    index = i;
                    break;
                }
            }

            for (int y = 0; y < Size; ++y)
                for (int x = 0; x < Size; ++x)
                {
                    if (Squares[x, y].Number == index)
                    {
                        Squares[x, y].Color = Colors.Red;
                        Squares[x, y].Number = 1;
                    }
                }
        }



        public int Size { get; set; }
        public SquareViewModel[,] Squares { get; set; }
    }
}

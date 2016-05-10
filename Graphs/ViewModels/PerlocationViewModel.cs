using Graphs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.ViewModels
{
    public class PerlocationViewModel
    {
        public PerlocationViewModel(Percolation percolation)
        {
            Size = percolation.size;
            Squares = new SquareViewModel[Size, Size];
            SortedSet<int> numbers = new SortedSet<int>();

            for(int y = 0; y < Size; ++y)
                for(int x = 0; x < Size; ++x)
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
                    Squares[x, y].Number = i;
                }
        }



        public int Size { get; set; }
        public SquareViewModel[,] Squares { get; set; }
    }
}

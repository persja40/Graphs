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

                    Squares[x, y].Number = i;
                }

            Dictionary<int, int> /*number, count*/ count = new Dictionary<int, int>();
            for (int y = 0; y < Size; ++y)
                for (int x = 0; x < Size; ++x)
                {
                    int number = Squares[x, y].Number;
                    if (number == 0)
                        continue;

                    if (count.ContainsKey(number) == false)
                        count.Add(number, 0);

                    count[number]++;
                }

            var max = count.Where(x => x.Value == count.Values.Max());

            for (int y = 0; y < Size; ++y)
                for (int x = 0; x < Size; ++x)
                {
                    int number = Squares[x, y].Number;
                    if (max.Where(pair => pair.Key == number).Count() > 0)
                    {
                        Squares[x, y].Color = Colors.Red;
                    }
                }
        }



        public int Size { get; set; }
        public SquareViewModel[,] Squares { get; set; }
    }
}

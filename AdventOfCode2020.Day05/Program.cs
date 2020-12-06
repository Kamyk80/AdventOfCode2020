using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day05
{
    internal class Program
    {
        private static void Main()
        {
            var seats = File.ReadAllLines("input.txt")
                .Select(SeatId)
                .OrderBy(i => i)
                .ToList();

            var last = seats.Last();
            var mine = seats.First();

            while (seats.Contains(mine)) mine++;

            Console.WriteLine(last);
            Console.WriteLine(mine);
            Console.ReadKey(true);
        }

        private static int SeatId(string seat)
        {
            var minRow = 0;
            var maxRow = 127;

            var minCol = 0;
            var maxCol = 7;

            foreach (var letter in seat)
            {
                switch (letter)
                {
                    case 'F':
                        maxRow -= (maxRow - minRow) / 2 + 1;
                        break;
                    case 'B':
                        minRow += (maxRow - minRow) / 2 + 1;
                        break;
                    case 'L':
                        maxCol -= (maxCol - minCol) / 2 + 1;
                        break;
                    case 'R':
                        minCol += (maxCol - minCol) / 2 + 1;
                        break;
                }
            }

            return minRow * 8 + minCol;
        }
    }
}

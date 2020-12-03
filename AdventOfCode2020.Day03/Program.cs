using System;
using System.IO;
using System.Linq;
using static System.Linq.Enumerable;

namespace AdventOfCode2020.Day03
{
    internal static class Program
    {
        private static string[] _area;

        private static void Main()
        {
            _area = File.ReadAllLines("input.txt");

            static int Trees(int right, int down) =>
                Range(0, _area.Length)
                    .Where(i => i % down == 0)
                    .Count(i => _area[i][i / down * right % _area[i].Length] == '#');

            var oneSlope = Trees(3, 1);
            var allSlopes = Trees(1, 1) * Trees(3, 1) * Trees(5, 1) * Trees(7, 1) * Trees(1, 2);

            Console.WriteLine(oneSlope);
            Console.WriteLine(allSlopes);
            Console.ReadKey(true);
        }
    }
}

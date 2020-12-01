using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day01
{
    internal static class Program
    {
        private static void Main()
        {
            var entries = File.ReadAllLines("input.txt")
                .Select(int.Parse)
                .ToList();

            var two = entries
                .SelectMany(x => entries, (x, y) => new {x, y})
                .Where(e => e.x + e.y == 2020)
                .Select(e => e.x * e.y)
                .First();

            var three = entries
                .SelectMany(x => entries, (x, y) => new {x, y})
                .SelectMany(e => entries, (e, z) => new {e.x, e.y, z})
                .Where(e => e.x + e.y + e.z == 2020)
                .Select(e => e.x * e.y * e.z)
                .First();

            Console.WriteLine(two);
            Console.WriteLine(three);
            Console.ReadKey(true);
        }
    }
}

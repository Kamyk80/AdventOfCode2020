using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day06
{
    internal class Program
    {
        private static void Main()
        {
            var groups = File.ReadAllText("input.txt").Split("\n\n")
                .Select(g => g.Split("\n"))
                .ToList();

            var anyone = groups
                .Sum(g => g
                    .SelectMany(a => a)
                    .Distinct()
                    .Count());

            var everyone = groups
                .Sum(g => g
                    .Select(a => a.AsEnumerable())
                    .Aggregate((a1, a2) => a1.Intersect(a2))
                    .Count());

            Console.WriteLine(anyone);
            Console.WriteLine(everyone);
            Console.ReadKey(true);
        }
    }
}

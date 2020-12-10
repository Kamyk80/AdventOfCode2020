using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day10
{
    internal class Program
    {
        private static void Main()
        {
            var adapters = File.ReadAllLines("input.txt")
                .Select(int.Parse)
                .OrderBy(a => a)
                .ToList();

            var differences = adapters
                .Aggregate(
                    (joltage: 0, oneDiffs: 0, threeDiffs: 1),
                    (current, joltage) => (
                        joltage,
                        current.oneDiffs += joltage - current.joltage == 1 ? 1 : 0,
                        current.threeDiffs += joltage - current.joltage == 3 ? 1 : 0),
                    result => result.oneDiffs * result.threeDiffs);

            var arrangements = Arrangements(adapters);

            Console.WriteLine(differences);
            Console.WriteLine(arrangements);
            Console.ReadKey(true);
        }

        private static long Arrangements(ICollection<int> adapters)
        {
            var previous = 0;
            var group = 1;

            long arrangements = 1;

            foreach (var adapter in adapters.Append(adapters.Last() + 3))
            {
                if (adapter - previous == 1)
                {
                    group++;
                }
                else
                {
                    arrangements *= group switch
                    {
                        3 => 2,
                        4 => 4,
                        5 => 7,
                        _ => 1
                    };

                    group = 1;
                }

                previous = adapter;
            }

            return arrangements;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day09
{
    internal class Program
    {
        private static void Main()
        {
            var numbers = File.ReadAllLines("input.txt")
                .Select(long.Parse)
                .ToList();

            var invalid = numbers
                .Skip(25)
                .Where((n, i) => numbers
                    .Skip(i)
                    .Take(25)
                    .SelectMany(n1 => numbers
                        .Skip(i)
                        .Take(25),
                        (n1, n2) => n1 + n2)
                    .All(s => s != n))
                .First();

            var weakness = FindWeakness(numbers, invalid);

            Console.WriteLine(invalid);
            Console.WriteLine(weakness);
            Console.ReadKey(true);
        }

        private static long FindWeakness(IList<long> numbers, long invalid)
        {
            for (var i = 0;; i++)
            {
                var sum = numbers[i];
                var min = numbers[i];
                var max = numbers[i];

                for (var j = i + 1;; j++)
                {
                    sum += numbers[j];
                    min = numbers[j] < min ? numbers[j] : min;
                    max = numbers[j] > max ? numbers[j] : max;

                    if (sum == invalid) return min + max;
                    if (sum > invalid) break;
                }
            }
        }
    }
}

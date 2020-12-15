using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day15
{
    internal class Program
    {
        private static void Main()
        {
            var numbers = "15,12,0,14,3,1"
                .Split(',')
                .Select(int.Parse)
                .ToList();

            Console.WriteLine(LastNumber(numbers, 2020));
            Console.WriteLine(LastNumber(numbers, 30000000));
            Console.ReadKey(true);
        }

        private static int LastNumber(ICollection<int> numbers, int rounds)
        {
            var prevNumbers = numbers
                .SkipLast(1)
                .Select((n, i) => (n, i))
                .ToDictionary(p => p.n, p => p.i);

            var lastNumber = numbers
                .Last();

            for (var round = numbers.Count; round < rounds; round++)
            {
                if (prevNumbers.TryGetValue(lastNumber, out var lastIndex))
                {
                    prevNumbers[lastNumber] = round - 1;
                    lastNumber = round - 1 - lastIndex;
                }
                else
                {
                    prevNumbers.Add(lastNumber, round - 1);
                    lastNumber = 0;
                }
            }

            return lastNumber;
        }
    }
}

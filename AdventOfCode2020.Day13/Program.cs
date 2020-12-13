using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day13
{
    internal class Program
    {
        private static void Main()
        {
            var input = File.ReadAllLines("input.txt")
                .ToList();

            var departure = int.Parse(input[0]);
            var buses = input[1].Split(',')
                .Select((s, i) => (id: s, offset: i))
                .Where(b => b.id != "x")
                .Select(b => (id: int.Parse(b.id!), b.offset))
                .ToList();

            var waitTime = buses
                .Select(b => Enumerable.Range(1, int.MaxValue)
                    .Select(i => (b.id, timestamp: b.id * i))
                    .First(b2 => b2.timestamp > departure))
                .OrderBy(b => b.timestamp)
                .Select(b => b.id * (b.timestamp - departure))
                .First();

            var sequenceTime = TimeUntilSequence(buses);

            Console.WriteLine(waitTime);
            Console.WriteLine(sequenceTime);
            Console.ReadKey(true);
        }

        private static long TimeUntilSequence(IList<(int id, int offset)> buses)
        {
            var index = 1;
            long time = 0;
            long increment = buses[0].id;

            while (index < buses.Count)
            {
                do time += increment; 
                while ((time + buses[index].offset) % buses[index].id != 0);

                increment *= buses[index].id;
                index++;
            }

            return time;
        }
    }
}

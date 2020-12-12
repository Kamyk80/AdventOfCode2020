using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day11
{
    internal class Program
    {
        private static readonly (int dx, int dy)[] Offsets = {(-1, -1), (0, -1), (1, -1), (-1, 0), (1, 0), (-1, 1), (0, 1), (1, 1)};

        private static void Main()
        {
            var seats = File.ReadAllLines("input.txt")
                .Select(l => l.ToArray())
                .ToArray();

            var occupiedFirstRule = Run(seats, OccupiedFirstRule, 4)
                .SelectMany(l => l)
                .Count(s => s == '#');

            var occupiedSecondRule = Run(seats, OccupiedSecondRule, 5)
                .SelectMany(l => l)
                .Count(s => s == '#');

            Console.WriteLine(occupiedFirstRule);
            Console.WriteLine(occupiedSecondRule);
            Console.ReadKey(true);
        }

        private static char[][] Run(char[][] seats, Func<char[][], (int, int), (int, int), bool> occupiedRule, int occupiedNumber)
        {
            bool changed;

            do
            {
                changed = false;

                var next = new char[seats.Length][];
                for (var y = 0; y < seats.Length; y++)
                {
                    next[y] = new char[seats[0].Length];
                    for (var x = 0; x < seats[0].Length; x++)
                    {
                        next[y][x] = seats[y][x];

                        var occupied = Offsets.Count(o => occupiedRule(seats, (x, y), o));

                        if (seats[y][x] == 'L' && occupied == 0)
                        {
                            next[y][x] = '#';
                            changed = true;
                        }

                        if (seats[y][x] == '#' && occupied >= occupiedNumber)
                        {
                            next[y][x] = 'L';
                            changed = true;
                        }
                    }
                }

                seats = next;
            } while (changed);

            return seats;
        }

        private static bool OccupiedFirstRule(char[][] seats, (int x, int y) pos, (int dx, int dy) offset) =>
            (x: pos.x + offset.dx, y: pos.y + offset.dy) switch
            {
                var (_, y) when y < 0 || y >= seats.Length => false,
                var (x, _) when x < 0 || x >= seats[0].Length => false,
                var (x, y) => seats[y][x] == '#'
            };

        private static bool OccupiedSecondRule(char[][] seats, (int x, int y) pos, (int dx, int dy) offset) =>
            Enumerable
                .Range(1, int.MaxValue)
                .Select(i => (x: pos.x + offset.dx * i, y: pos.y + offset.dy * i))
                .Select(p => (p.x, p.y, exists: p.y >= 0 && p.y < seats.Length && p.x >= 0 && p.x < seats[0].Length))
                .Select(p => (p.exists, seat: p.exists ? seats[p.y][p.x] : ' '))
                .First(p => p.seat != '.' || !p.exists).seat == '#';
    }
}

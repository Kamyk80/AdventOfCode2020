using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day12
{
    internal class Program
    {
        private static void Main()
        {
            var instructions = File.ReadAllLines("input.txt")
                .Select(l => (action: l[0], value: int.Parse(l.Substring(1))))
                .ToList();

            var distShip = MoveShip(instructions);
            var distWaypoint = MoveWaypoint(instructions);

            Console.WriteLine(distShip);
            Console.WriteLine(distWaypoint);
            Console.ReadKey(true);
        }

        private static int MoveShip(IEnumerable<(char action, int value)> instructions)
        {
            var ship = (x: 0, y: 0);
            var dir = (x: 1, y: 0);

            foreach (var (action, value) in instructions)
            {
                switch (action)
                {
                    case 'N':
                        ship.y += value;
                        break;
                    case 'S':
                        ship.y -= value;
                        break;
                    case 'E':
                        ship.x += value;
                        break;
                    case 'W':
                        ship.x -= value;
                        break;
                    case 'L':
                        dir = Rotate(dir, 360 - value);
                        break;
                    case 'R':
                        dir = Rotate(dir, value);
                        break;
                    case 'F':
                        ship.x += dir.x * value;
                        ship.y += dir.y * value;
                        break;
                }
            }

            return Math.Abs(ship.x) + Math.Abs(ship.y);
        }

        private static int MoveWaypoint(IEnumerable<(char action, int value)> instructions)
        {
            var waypoint = (x: 10, y: 1);
            var ship = (x: 0, y: 0);

            foreach (var (action, value) in instructions)
            {
                switch (action)
                {
                    case 'N':
                        waypoint.y += value;
                        break;
                    case 'S':
                        waypoint.y -= value;
                        break;
                    case 'E':
                        waypoint.x += value;
                        break;
                    case 'W':
                        waypoint.x -= value;
                        break;
                    case 'L':
                        waypoint = Rotate(waypoint, 360 - value);
                        break;
                    case 'R':
                        waypoint = Rotate(waypoint, value);
                        break;
                    case 'F':
                        ship.x += waypoint.x * value;
                        ship.y += waypoint.y * value;
                        break;
                }
            }

            return Math.Abs(ship.x) + Math.Abs(ship.y);
        }

        private static (int x, int y) Rotate((int x, int y) point, int value) =>
            value switch
            {
                90 => (point.y, -point.x),
                180 => (-point.x, -point.y),
                270 => (-point.y, point.x),
                _ => throw new ArgumentException()
            };
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day08
{
    internal class Program
    {
        private static void Main()
        {
            var program = File.ReadAllLines("input.txt")
                .Select(l => l.Split(' '))
                .Select(s => (op: s[0], arg: int.Parse(s[1])))
                .ToList();

            var (infiniteResult, _) = Run(program);
            var (correctResult, _) = program
                .Where(l => l.op != "acc")
                .Select(l1 => program
                    .Select(l2 => l2 == l1
                        ? l2 switch
                        {
                            { op: "jmp" } => ("nop", l2.arg),
                            { op: "nop" } => ("jmp", l2.arg),
                            _ => l2
                        }
                        : l2)
                    .ToList())
                .Select(Run)
                .Single(r => r.finished);

            Console.WriteLine(infiniteResult);
            Console.WriteLine(correctResult);
            Console.ReadKey(true);
        }

        private static (int acc, bool finished) Run(IList<(string op, int arg)> program)
        {
            var acc = 0;
            var line = 0;

            var visited = new HashSet<int>();

            while (line < program.Count && visited.Add(line))
            {
                var (op, arg) = program[line];

                switch (op)
                {
                    case "acc":
                        acc += arg;
                        break;
                    case "jmp":
                        line += arg;
                        continue;
                    case "nop":
                        break;
                }

                line++;
            }

            return (acc, line >= program.Count);
        }
    }
}

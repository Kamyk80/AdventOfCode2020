using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day14
{
    internal class Program
    {
        private static readonly Regex MaskRegex = new Regex("^mask = (?<mask>[01X]+)$");
        private static readonly Regex MemRegex = new Regex("^mem\\[(?<adr>[0-9]+)\\] = (?<val>[0-9]+)$");

        private static void Main()
        {
            var program = File.ReadAllLines("input.txt")
                .ToList();

            var sumVersion1 = RunVersion1(program)
                .Sum(m => m.Value);

            var sumVersion2 = RunVersion2(program)
                .Sum(m => m.Value);

            Console.WriteLine(sumVersion1);
            Console.WriteLine(sumVersion2);
            Console.ReadKey(true);
        }

        private static Dictionary<long, long> RunVersion1(IEnumerable<string> program)
        {
            var memory = new Dictionary<long, long>();
            var mask = string.Empty;

            foreach (var instruction in program)
            {
                if (instruction.StartsWith("mask"))
                {
                    mask = MaskRegex.Match(instruction).Groups["mask"].Value;
                }

                if (instruction.StartsWith("mem"))
                {
                    var match = MemRegex.Match(instruction);
                    var address = long.Parse(match.Groups["adr"].Value);
                    var value = long.Parse(match.Groups["val"].Value);

                    for (var bit = 0; bit < mask.Length; bit++)
                    {
                        if (mask[mask.Length - 1 - bit] == '0')
                        {
                            value &= ~(1L << bit);
                        }

                        if (mask[mask.Length - 1 - bit] == '1')
                        {
                            value |= 1L << bit;
                        }
                    }

                    if (!memory.TryAdd(address, value))
                    {
                        memory[address] = value;
                    }
                }
            }

            return memory;
        }

        private static Dictionary<long, long> RunVersion2(IEnumerable<string> program)
        {
            var memory = new Dictionary<long, long>();
            var mask = string.Empty;

            foreach (var instruction in program)
            {
                if (instruction.StartsWith("mask"))
                {
                    mask = MaskRegex.Match(instruction).Groups["mask"].Value;
                }

                if (instruction.StartsWith("mem"))
                {
                    var match = MemRegex.Match(instruction);
                    var address = long.Parse(match.Groups["adr"].Value);
                    var value = long.Parse(match.Groups["val"].Value);

                    for (var bit = 0; bit < mask.Length; bit++)
                    {
                        if (mask[mask.Length - 1 - bit] == '1')
                        {
                            address |= 1L << bit;
                        }
                    }

                    var addressesToAdd = new HashSet<long> {address};

                    for (var bit = 0; bit < mask.Length; bit++)
                    {
                        if (mask[mask.Length - 1 - bit] == 'X')
                        {
                            foreach (var addressToAdd in addressesToAdd.ToList())
                            {
                                addressesToAdd.Add(addressToAdd | 1L << bit);
                                addressesToAdd.Add(addressToAdd & ~(1L << bit));
                            }
                        }
                    }

                    foreach (var addressToAdd in addressesToAdd)
                    {
                        if (!memory.TryAdd(addressToAdd, value))
                        {
                            memory[addressToAdd] = value;
                        }
                    }
                }
            }

            return memory;
        }
    }
}

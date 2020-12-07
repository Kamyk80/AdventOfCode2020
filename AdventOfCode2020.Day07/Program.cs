using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Linq.Enumerable;

namespace AdventOfCode2020.Day07
{
    internal class Program
    {
        private static Dictionary<string, Dictionary<string, int>> _rules;

        private static void Main()
        {
            var regex = new Regex(@"^(?<mainbag>[a-z ]+) bags contain(?<contents> (?<itemcount>[0-9]+) (?<itembag>[a-z ]+) bags?[,.])*| no other bags.$");

            _rules = File.ReadAllLines("input.txt")
                .Select(l => regex.Match(l))
                .ToDictionary(
                    m => m.Groups["mainbag"].Value,
                    m => Range(0, m.Groups["contents"].Captures.Count)
                        .ToDictionary(
                            i => m.Groups["itembag"].Captures[i].Value,
                            i => int.Parse(m.Groups["itemcount"].Captures[i].Value)));

            var bagsContain = _rules.Count(r => ContainsShinyGoldBag(r.Value));
            var bagsInside = BagsInsideCount(_rules["shiny gold"]);

            Console.WriteLine(bagsContain);
            Console.WriteLine(bagsInside);
            Console.ReadKey(true);
        }

        private static bool ContainsShinyGoldBag(IDictionary<string, int> contents) =>
            contents.ContainsKey("shiny gold") || contents.Any(i => ContainsShinyGoldBag(_rules[i.Key]));

        private static int BagsInsideCount(IDictionary<string, int> contents) =>
            contents.Sum(i => i.Value) + contents.Sum(i => BagsInsideCount(_rules[i.Key]) * i.Value);
    }
}

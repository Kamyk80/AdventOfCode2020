using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day02
{
    internal static class Program
    {
        private static void Main()
        {
            var regex = new Regex(@"^(?<first>\d+)-(?<second>\d+) (?<letter>.): (?<password>.+)$");
            var passwords = File.ReadAllLines("input.txt")
                .Select(l => regex.Match(l))
                .Select(m => new
                {
                    First = int.Parse(m.Groups["first"].Value),
                    Second = int.Parse(m.Groups["second"].Value),
                    Letter = m.Groups["letter"].Value[0],
                    Password = m.Groups["password"].Value
                })
                .ToList();

            var valid1 = passwords
                .Count(p => p.Password.Count(c => c == p.Letter) >= p.First && p.Password.Count(c => c == p.Letter) <= p.Second);

            var valid2 = passwords
                .Count(p => p.Password[p.First - 1] == p.Letter ^ p.Password[p.Second - 1] == p.Letter);

            Console.WriteLine(valid1);
            Console.WriteLine(valid2);
            Console.ReadKey(true);
        }
    }
}

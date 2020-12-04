using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day04
{
    internal static class Program
    {
        private static readonly Regex YearRegex = new Regex(@"^(byr|iyr|eyr):(?<year>\d{4})$");
        private static readonly Regex HeightRegex = new Regex(@"^hgt:(?<value>\d{2,3})(?<unit>cm|in)$");
        private static readonly Regex HairColorRegex = new Regex(@"^hcl:#[0-9a-f]{6}$");
        private static readonly Regex EyeColorRegex = new Regex(@"^ecl:(amb|blu|brn|gry|grn|hzl|oth)$");
        private static readonly Regex PassportIdRegex = new Regex(@"^pid:\d{9}$");

        private static void Main()
        {
            var passports = File.ReadAllText("input.txt").Split("\n\n")
                .Select(p => p.Split(new[] {" ", "\n"}, StringSplitOptions.None))
                .ToList();

            var valid1 = passports
                .Count(p =>
                    p.Any(f => f.StartsWith("byr:")) &&
                    p.Any(f => f.StartsWith("iyr:")) &&
                    p.Any(f => f.StartsWith("eyr:")) &&
                    p.Any(f => f.StartsWith("hgt:")) &&
                    p.Any(f => f.StartsWith("hcl:")) &&
                    p.Any(f => f.StartsWith("ecl:")) &&
                    p.Any(f => f.StartsWith("pid:")));

            var valid2 = passports
                .Count(p =>
                    p.SingleOrDefault(f => f.StartsWith("byr:")).IsBirthYearValid() &&
                    p.SingleOrDefault(f => f.StartsWith("iyr:")).IsIssueYearValid() &&
                    p.SingleOrDefault(f => f.StartsWith("eyr:")).IsExpYearValid() &&
                    p.SingleOrDefault(f => f.StartsWith("hgt:")).IsHeightValid() &&
                    p.SingleOrDefault(f => f.StartsWith("hcl:")).IsHairColorValid() &&
                    p.SingleOrDefault(f => f.StartsWith("ecl:")).IsEyeColorValid() &&
                    p.SingleOrDefault(f => f.StartsWith("pid:")).IsPassportIdValid());

            Console.WriteLine(valid1);
            Console.WriteLine(valid2);
            Console.ReadKey(true);
        }

        private static bool IsBirthYearValid(this string text) => IsYearValid(text, 1920, 2002);

        private static bool IsIssueYearValid(this string text) => IsYearValid(text, 2010, 2020);

        private static bool IsExpYearValid(this string text) => IsYearValid(text, 2020, 2030);

        private static bool IsYearValid(string text, int min, int max)
        {
            if (text == null) return false;

            var match = YearRegex.Match(text);
            if (!match.Success) return false;

            var number = int.Parse(match.Groups["year"].Value);
            return number >= min && number <= max;
        }

        private static bool IsHeightValid(this string text)
        {
            if (text == null) return false;

            var match = HeightRegex.Match(text);
            if (!match.Success) return false;

            var value = int.Parse(match.Groups["value"].Value);
            var unit = match.Groups["unit"].Value;
            return unit switch
            {
                "cm" => value >= 150 && value <= 193,
                "in" => value >= 59 && value <= 76,
                _ => false
            };
        }

        private static bool IsHairColorValid(this string text) => text != null && HairColorRegex.Match(text).Success;

        private static bool IsEyeColorValid(this string text) => text != null && EyeColorRegex.Match(text).Success;

        private static bool IsPassportIdValid(this string text) => text != null && PassportIdRegex.Match(text).Success;
    }
}

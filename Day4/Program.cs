﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day4
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            var passports = (await File.ReadAllTextAsync("input.txt")).Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Replace("\n", " ")).ToArray();
            Part1(passports);
            Part2(passports);
        }

        static void Part1(string[] passports)
        {
            Console.WriteLine(passports.Count(x => Passport.TryParse(x)));
        }

        static void Part2(string[] passports)
        {
            Console.WriteLine(passports.Count(Passport.Validate));
        }
    }

    class Passport
    {
        public static bool Validate(string value)
        {
            var valid = false;
            var args = value.Split(new[] { '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(":", StringSplitOptions.RemoveEmptyEntries))
                .ToDictionary(x => x[0], x => x[1]);
            try
            {
                valid = int.Parse(args["byr"]) is var byr && byr >= 1920 && byr <= 2002
                        && int.Parse(args["iyr"]) is var iyr && iyr >= 2010 && iyr <= 2020
                        && int.Parse(args["eyr"]) is var eyr && eyr >= 2020 && eyr <= 2030
                        && ((args["hgt"].LastIndexOf("cm") is var cmPos
                             && cmPos > 0
                             && int.Parse(args["hgt"].Substring(0, cmPos)) is var cm
                             && cm >= 150
                             && cm <= 193)
                            || (args["hgt"].LastIndexOf("in") is var inPos
                                && inPos > 0
                                && int.Parse(args["hgt"].Substring(0, inPos)) is var inches
                                && inches >= 59
                                && inches <= 76))
                        && Regex.IsMatch(args["hcl"], "#(\\d|[a-f]){6}")
                        && new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Any(x => x == args["ecl"])
                        && Regex.IsMatch(args["pid"], "\\b[0-9]{9}\\b");
            }
            catch (KeyNotFoundException knfe)
            {
                valid = false;
            }
            catch (Exception e)
            {
                valid = false;
                Console.WriteLine(e);
            }

            return valid;
        }

        public static bool TryParse(string value)
        {
            var args = value.Split(new[] { '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(":", StringSplitOptions.RemoveEmptyEntries))
                .ToDictionary(x => x[0], x => x[1]);

            try
            {
                return args.ContainsKey("byr") &&
                       args.ContainsKey("iyr") &&
                       args.ContainsKey("eyr") &&
                       args.ContainsKey("hgt") &&
                       args.ContainsKey("hcl") &&
                       args.ContainsKey("ecl") &&
                       args.ContainsKey("pid");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine(value);
                foreach (var arg in args)
                {
                    Console.WriteLine($"{arg.Key}: {arg.Value}");
                }

                Console.WriteLine();
                return false;
            }
        }
    }
}
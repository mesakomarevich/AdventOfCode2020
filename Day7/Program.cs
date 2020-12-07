using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day7
{
    class Program
    {
        private const string sg = "shiny gold";

        static async Task Main(string[] args)
        {
            var data = (await File.ReadAllLinesAsync("input.txt"));

            Part1(data);
            Part2(data);
        }

        static void Part1(string[] data)
        {
            var bags = ParseBags(data);

            //var holdGold = bags.Where(x => x.Value.Contains(sg)).ToList();
            var holdGold = TraverseBags(bags, new HashSet<string>(), sg);
            Console.WriteLine(holdGold.Count());

            static Dictionary<string, List<string>> ParseBags(string[] data)
            {
                var bags = new Dictionary<string, List<string>>();

                foreach (var bag in data)
                {
                    var tokens = bag.Split(" bags contain ");
                    var name = tokens[0];
                    var contains = tokens[1].Split(new string[] { "bags, ", "bag, " }, StringSplitOptions.RemoveEmptyEntries).Select(x => x
                            .Substring(2)
                            .Replace("bags", "")
                            .Replace("bag", "")
                            .Replace(".", "")
                            .Trim())
                        .ToList();
                    bags.Add(name, contains);
                }

                return bags;
            }

            static List<string> TraverseBags(Dictionary<string, List<string>> bags, HashSet<string> holdGold, string current)
            {
                var check = bags.Where(x => x.Value.Contains(current)).Select(x => x.Key).ToList();
                foreach (var toCheck in check)
                {
                    if (holdGold.Add(toCheck))
                    {
                        TraverseBags(bags, holdGold, toCheck);
                    }
                }

                return holdGold.ToList();
            }
        }


        static void Part2(string[] data)
        {
            var bags = ParseBags(data);

            Console.WriteLine(TraverseBags(bags, sg) - 1);

            static int TraverseBags(Dictionary<string, List<(string, int)>> bags, string current)
            {
                var count = 1;

                if (bags.TryGetValue(current, out var check))
                {
                    foreach (var toCheck in check)
                    {
                        count += toCheck.Item2 * TraverseBags(bags, toCheck.Item1);
                    }
                }

                return count;
            }

            static Dictionary<string, List<(string, int)>> ParseBags(string[] data)
            {
                var bags = new Dictionary<string, List<(string, int)>>();

                foreach (var bag in data)
                {
                    var tokens = bag.Split(" bags contain ");
                    var name = tokens[0];
                    var contains = tokens[1].Split(new string[] { "bags, ", "bag, " }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x =>
                        {
                            var toks = x.Trim().Split(" ", 2, StringSplitOptions.RemoveEmptyEntries);
                            var name = "";
                            if (int.TryParse(toks[0], out var num))
                            {
                                name = toks[1].Replace("bags", "")
                                    .Replace("bag", "")
                                    .Replace(".", "")
                                    .Trim();
                            }

                            return (name, num);
                        })
                        .ToList();
                    bags.Add(name, contains);
                }

                return bags;
            }
        }
    }
}
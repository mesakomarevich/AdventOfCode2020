using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day6
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var groups = (await File.ReadAllTextAsync("input.txt")).Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
            Part1(groups);
            Part2(groups);
        }

        static void Part1(string[] groups)
        {
            Console.WriteLine(groups.Sum(x => x.Replace("\n", "").Distinct().Count()));
        }

        static void Part2(string[] groups)
        {
            Console.WriteLine(groups.Sum(x => x.Split("\n").Aggregate((curr, next) => new string(curr.Intersect(next).ToArray())).Length));
        }
    }
}
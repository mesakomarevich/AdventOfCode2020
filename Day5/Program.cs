using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day5
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var passes = (await File.ReadAllLinesAsync("input.txt"));
            Part1(passes);
            Part2(passes);
        }

        static void Part1(string[] passes)
        {
            Console.WriteLine(passes.Max(x => (FindRow(x) * 8) + FindColumn(x)));
        }

        static void Part2(string[] passes)
        {
            var ids = passes.Select(x => (FindRow(x) * 8) + FindColumn(x)).OrderBy(x => x).ToArray();
            for (int i = 0; i < ids.Length - 1; i++)
            {
                if (ids[i + 1] - ids[i] == 2)
                {
                    Console.WriteLine(ids[i]+1);
                    return;
                }
            }
        }

        static int FindRow(string pass)
        {
            var dirs = pass.Substring(0, 7);
            var upper = 127;
            var lower = 0;

            foreach (var dir in dirs)
            {
                if (dir == 'F')
                {
                    upper -= (upper - lower + 1) / 2;
                }
                else
                {
                    lower += (upper - lower + 1) / 2;
                }
            }

            return upper;
        }

        static int FindColumn(string pass)
        {
            var dirs = pass.Substring(7);

            var upper = 7;
            var lower = 0;

            foreach (var dir in dirs)
            {
                if (dir == 'L')
                {
                    upper -= (upper - lower + 1) / 2;
                }
                else
                {
                    lower += (upper - lower + 1) / 2;
                }
            }

            return upper;
        }
    }
}
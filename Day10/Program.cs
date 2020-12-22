using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Day10
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var nums = (await File.ReadAllLinesAsync("input.txt"))
                .Select(x => int.Parse(x))
                .OrderBy(x => x)
                .ToArray();
            Part1(nums);
            Part2(nums.Select(x => (long)x).ToArray());
        }

        static void Part1(int[] nums)
        {
            var result = FindChain(0, nums);

            var jolts1 = 0;
            var jolts3 = 0;

            for (int i = 0; i < result.Length - 1; i++)
            {
                var diff = result[i + 1] - result[i];
                if (diff == 1)
                {
                    jolts1++;
                }

                if (diff == 3)
                {
                    jolts3++;
                }
            }

            Console.WriteLine(jolts1);
            Console.WriteLine(jolts3);
            Console.WriteLine(jolts1 * jolts3);

            static int[] FindChain(int current, int[] nums)
            {
                if (nums.Length == 0)
                {
                    return new[] { current, current + 3 };
                }

                var nexts = nums.Where(x => x - current >= 0 && x - current <= 3).ToArray();

                foreach (var next in nexts)
                {
                    var result = FindChain(next, nums.Except(new[] { next }).ToArray());
                    if (result != null)
                    {
                        return new int[] { current }.Concat(result).ToArray();
                    }
                }

                return null;
            }
        }
        

        static void Part2(long[] nums)
        {
            var max = nums.Max();
            var dict = nums.ToDictionary(x => x, x => -1L);
            var result = FindChain(0, nums);

            Console.WriteLine(result);

            long FindChain(long current, long[] nums)
            {
                if (nums.Length == 0 || current == max)
                {
                    return 1;
                }
                
                var nexts = nums.Where(x => x - current >= 0 && x - current <= 3).ToArray();
                long total = 0;
                
                foreach (var next in nexts)
                {
                    if (dict[next] != -1)
                    {
                        total += dict[next];
                        continue;
                    }
                    total += FindChain(next, nums.Except(new[] { next }).ToArray());
                }

                dict[current] = total;
                
                return total;
            }
        }
    }
}
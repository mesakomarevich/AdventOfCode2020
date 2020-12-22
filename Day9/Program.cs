using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day9
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var nums = (await File.ReadAllLinesAsync("input.txt")).Select(x => long.Parse(x)).ToArray();
            Part1(nums);
            Part2(nums, 1038347917);
        }

        static void Part1(long[] nums)
        {
            const int preamble = 25;
            int curr = preamble;
            int i = 0;
            int n = 0;
            var found = false;
            for (curr = preamble; curr < nums.Length; curr++)
            {
                found = false;
                for (i = curr - preamble; i < curr && found is not true; i++)
                {
                    for (n = (i + 1); n < curr; n++)
                    {
                        if (nums[i] + nums[n] == nums[curr])
                        {
                            found = true;
                            break;
                        }
                    }
                }
                if (found is not true)
                {
                    Console.WriteLine($"i: {i}");
                    Console.WriteLine($"n: {n}");
                    Console.WriteLine($"curr: {curr}");
                    Console.WriteLine($"nums[{i}]: {nums[i]}");
                    Console.WriteLine($"nums[{n}]: {nums[n]}");
                    Console.WriteLine($"nums[{curr}]: {nums[curr]}");
                    break;
                }
            }
        }

        static void Part2(long[] nums, long target)
        {
            int i = 0;
            int n = 0;
            for (i = 0; i < nums.Length; i++)
            {
                for (n = i + 1; n < nums.Length; n++)
                {
                    var range = nums.Skip(i).Take(n - i);
                    if (range.Sum() == target)
                    {
                        Console.WriteLine(range.Min() + range.Max());
                        return;
                    }
                }
            }
        }
    }
}
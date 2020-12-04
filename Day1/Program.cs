using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var lines = await File.ReadAllLinesAsync("input.txt");
            var nums = lines.Select(x => int.Parse(x)).ToArray();

            Part1(nums);
            Part2(nums);
        }

        static void Part1(int[] nums)
        {
            var primary = 0;
            int secondary = 0;

            for (int i = 0; i < nums.Length - 1; i++)
            {
                primary = nums[i];
                for (int n = i + 1; n < nums.Length; n++)
                {
                    secondary = nums[n];
                    if (primary + secondary == 2020)
                    {
                        Console.WriteLine($"{primary} + {secondary} == {primary + secondary}\n{primary} * {secondary} == {primary * secondary}");
                        return;
                    }
                }
            }
        }

        static void Part2(int[] nums)
        {
            var primary = 0;
            var secondary = 0;
            var tertiary = 0;

            for (var i = 0; i < nums.Length - 2; i++)
            {
                primary = nums[i];
                for (var n = i + 1; n < nums.Length - 1; n++)
                {
                    secondary = nums[n];
                    for (var j = n + 1; j < nums.Length; j++)
                    {
                        tertiary = nums[j];
                        if (primary + secondary + tertiary == 2020)
                        {
                            Console.WriteLine($"{primary} + {secondary} + {tertiary} == {primary + secondary + tertiary}\n"
                                              + $"{primary} * {secondary} * {tertiary} == {primary * secondary * tertiary}");
                            return;
                        }
                    }
                }
            }
        }
    }
}
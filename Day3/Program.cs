using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var lines = await File.ReadAllLinesAsync("input.txt");
            var map = lines.Select(x => x.ToCharArray()).ToArray();
            Part1(map);
            Part2(map);
        }

        static void Part1(char[][] map)
        {
            Console.WriteLine(TreeCollisionFinder(map, 3, 1));
        }
        
        static void Part2(char[][] map)
        {
            var slopes = new[]
            {
                (1, 1),
                (3, 1),
                (5, 1),
                (7, 1),
                (1, 2)
            };
            
            Console.WriteLine(slopes.Aggregate(1L, (sum, slope) => sum *= TreeCollisionFinder(map, slope.Item1, slope.Item2)));
        }

        static int TreeCollisionFinder(char[][] map, int xIncrement, int yIncrement)
        {
            var height = map.Length;
            var width = map[0].Length;
            var count = 0;
            for (int x = 0, y = 0; y < height; x += xIncrement, y += yIncrement)
            {
                if (x >= width)
                {
                    x -= width;
                }
                count += map[y][x] == '#' ? 1 : 0;
            }

            return count;
        }
    }
}
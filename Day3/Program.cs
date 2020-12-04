using System;
using System.IO;
using System.Linq;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = await File.ReadAllLinesAsync("input.txt");
            var map = lines.Select(x => x.ToCharArray());
        }

        static void Part1(char[,] map)
        {
            
        }
    }
}
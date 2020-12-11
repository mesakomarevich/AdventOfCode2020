using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day8
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var lines = (await File.ReadAllLinesAsync("input.txt")).Select(x => new KeyValuePair<string, bool>(x, false)).ToList();

            Part1(lines);
            //Part2();
        }

        static void Part1(List<KeyValuePair<string, bool>> lines)
        {
            var i = 0;
            var acc = 0;
            
            while(i < lines.Count)
            {
                if (lines[i].Value)
                {
                    Console.WriteLine(acc);
                    break;
                }
                else
                {
                    lines[i] = new KeyValuePair<string, bool>(lines[i].Key, true);
                }

                
                var tokens = lines[i].Key.Split(" ");
                var cmd = tokens[0];
                var arg = tokens[1];
                
                Console.WriteLine(i);
                Console.WriteLine(lines[i].Key);
                if (cmd == "nop")
                {
                    i++;
                }
                else if (cmd == "acc")
                {
                    i++;
                    acc += arg[0] == '+' ? int.Parse(arg.Substring(1)) : int.Parse(arg);
                }
                else
                {
                    i += arg[0] == '+' ? int.Parse(arg.Substring(1)) : int.Parse(arg);
                }
            }

            if (i >= lines.Count)
            {
                Console.WriteLine($"acc: {acc}");
            }
        }

        static void Part2(List<KeyValuePair<string, bool>> lines)
        {
            
        }
    }
}
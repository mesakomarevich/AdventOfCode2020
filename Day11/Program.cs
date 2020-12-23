using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day11
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var temp = (await File.ReadAllLinesAsync("input.txt")).Select(x => x.ToArray()).ToArray();
            var seats = new char[temp.Length, temp[0].Length];
            for (int i = 0; i < temp.Length; i++)
            {
                for (int n = 0; n < temp[i].Length; n++)
                {
                    seats[i, n] = temp[i][n];
                }
            }

            Part1(seats);
        }

        static void Part1(char[,] seats)
        {
            var changes = 0;
            var yMax = seats.GetLength(0);
            var xMax = seats.GetLength(1);

            //var newSeats = new char[yMax, xMax];

            //PrintLayout();
            
            do
            {
                var newSeats = new char[yMax, xMax];
                changes = 0;
                for (int y = 0; y < yMax; y++)
                {
                    for (int x = 0; x < xMax; x++)
                    {
                        var pos = seats[y, x];
                        if (pos == '.')
                        {
                            newSeats[y, x] = '.';
                        }
                        else if (pos == 'L')
                        {
                            newSeats[y, x] = CheckEmpty(y, x) ? '#' : 'L';
                        }
                        else
                        {
                            newSeats[y, x] = CheckOccupied(y, x) ? '#' : 'L';
                        }

                        if (seats[y, x] != newSeats[y, x])
                        {
                            changes++;
                        }
                    }
                }

                seats = newSeats;
                //PrintLayout();
            } while (changes > 0);

            var total = 0;
            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    if (seats[y, x] == '#')
                    {
                        total++;
                    }

                    Console.Write(seats[y,x]);
                }
                Console.WriteLine();
            }

            Console.WriteLine(total);

            void PrintLayout()
            {
                for (int y = 0; y < yMax; y++)
                {
                    for (int x = 0; x < xMax; x++)
                    {
                        Console.Write(seats[y,x]);
                    }
                    Console.WriteLine();
                }

                Console.WriteLine();
            }
            
            bool CheckPos(int y, int x, out char val)
            {
                val = '\0';

                if (y >= 0 && y < yMax && x >= 0 && x < xMax)
                {
                    val = seats[y, x];
                    return true;
                }

                return false;
            }

            bool CheckEmpty(int y, int x)
            {
                var empty = true;

                for (int i = -1; i < 2 && empty; i++)
                {
                    for (int n = -1; n < 2 && empty; n++)
                    {
                        if (i == 0 && n == 0)
                        {
                            continue;
                        }
                        if (CheckPos(y + i, x + n, out var val))
                        {
                            if (val == '#')
                            {
                                empty = false;
                                break;
                            }
                        }
                    }
                }

                return empty;
            }

            bool CheckOccupied(int y, int x)
            {
                int occupied = 0;
                for (int i = -1; i < 2 && occupied < 4; i++)
                {
                    for (int n = -1; n < 2 && occupied < 4; n++)
                    {
                        if (i == 0 && n == 0)
                        {
                            continue;
                        }
                        if (CheckPos(y + i, x + n, out var val))
                        {
                            if (val == '#')
                            {
                                occupied++;
                            }
                        }
                    }
                }

                return occupied < 4;
            }
        }
    }
}
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var policies = (await File.ReadAllLinesAsync("input.txt")).Select(x => PasswordPolicy.Parse(x));

            Console.WriteLine(policies.Count(x => x.CheckPasswordPart1()));
            Console.WriteLine(policies.Count(x => x.CheckPasswordPart2()));
        }

        class PasswordPolicy
        {
            public int Min { get; set; }
            public int Max { get; set; }
            public char Letter { get; set; }
            public string Password { get; set; }

            public PasswordPolicy(int min, int max, char letter, string password)
            {
                Min = min;
                Max = max;
                Letter = letter;
                Password = password;
            }

            public bool CheckPasswordPart1()
            {
                var occurrences = Password.ToCharArray().Count(x => x == Letter);
                return Min <= occurrences && occurrences <= Max;
            }

            public bool CheckPasswordPart2()
            {
                return Password[Min - 1] == Letter ^ Password[Max - 1] == Letter;
            }

            public static PasswordPolicy Parse(string value)
            {
                var tokens = value.Split('-', 2, StringSplitOptions.RemoveEmptyEntries);
                var min = int.Parse(tokens[0]);
                tokens = tokens[1].Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
                var max = int.Parse(tokens[0]);
                tokens = tokens[1].Split(':', 2, StringSplitOptions.RemoveEmptyEntries);
                var letter = char.Parse(tokens[0]);
                var password = tokens[1].Trim();
                
                return new PasswordPolicy(min, max, letter, password);
            }
        }
    }
}
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace PasswordValidator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var filePath = @"E:\my_project\cSharp\TEST\PasswordValidator\PasswordValidator\Files\passwords.txt";
            var fileInfo = new FileInfo(filePath);

            if (fileInfo.Exists)
            {
                var validPasswordsCount = CountValidPasswords(filePath);
                Console.WriteLine($"The number of valid passwords: {validPasswordsCount}");
            }
            else
            {
                Console.WriteLine("File not found! " + filePath);
            }

            Console.ReadKey();

        }

        static int CountValidPasswords(string filePath)
        {
            int validPasswordsCount = 0;

            foreach (string line in File.ReadLines(filePath))
            {
                var match = Regex.Match(line, @"(.+) (\d+)-(\d+): (.*)");
                if (!match.Success)
                    continue;

                string symbol = match.Groups[1].Value;
                int minCount = int.Parse(match.Groups[2].Value);
                int maxCount = int.Parse(match.Groups[3].Value);
                string password = match.Groups[4].Value;

                if (IsValidPassword(symbol, minCount, maxCount, password))
                    validPasswordsCount++;
            }

            return validPasswordsCount;
        }

        static bool IsValidPassword(string requiredSymbol, int minCount, int maxCount, string password)
        {
            int charCount = password.Split(requiredSymbol).Length - 1;

            return charCount >= minCount && charCount <= maxCount;
        }
    }
}

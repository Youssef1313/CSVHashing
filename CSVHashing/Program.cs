using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CSVHashing
{
    internal class Program
    {
        private const string FileName = "annual-enterprise-survey-2020-financial-year-provisional-csv.csv"; // "testcase2.csv";
        private const int TargetColumnIndex = 2;

        private static int Main()
        {
            if (!File.Exists(FileName))
            {
                var fullFileName = Path.Combine(Directory.GetCurrentDirectory(), FileName);
                Console.WriteLine($"Cannot find the file '{fullFileName}'");
                return 1;
            }

            using var streamReader = new StreamReader(FileName);
            var csvReader = new CsvReader(streamReader);

            // Ignore the column names.
            _ = csvReader.ReadNextLine();
            var builder = new StringBuilder();

            string? data;
            while ((data = csvReader.ReadNextLineAtColumn(TargetColumnIndex)) is not null)
            {
                builder.Append(data);

                // Ignore even rows
                if (csvReader.ReadNextLine() is null)
                {
                    break;
                }
            }

            Console.WriteLine(HashMD5(builder.ToString()));
            return 0;
        }

        private static string HashMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using var md5 = MD5.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            var builder = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                builder.Append(hashBytes[i].ToString("X2"));
            }

            return builder.ToString();
        }

    }
}

using System.Text.RegularExpressions;
using Utilities.IO;

namespace DayThree;

internal class Program
{
    static void Main()
    {
        string rawData = FileUtilities.GetRawData("input.txt");

        var trimmedMatches = Regex.Matches(rawData, @"mul\(\d+,\d+\)")
                                  .Select(m => m.ToString()[4..^1]);

        var totalSum = 0;
        foreach (var match in trimmedMatches)
        {
            int[] splitMatches = match.Split(',')
                                      .Select(s => int.Parse(s))
                                      .ToArray();

            int product = splitMatches[0] * splitMatches[1];
            totalSum += product;
        }

        Console.WriteLine(totalSum);
    }
}

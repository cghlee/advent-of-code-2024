using System.Text.RegularExpressions;
using Utilities.IO;

namespace DayThree;

internal class Program
{
    static void Main()
    {
        string rawData = FileUtilities.GetRawData("input.txt");


        /* Below code not needed for Part 1 solution */
        string[] stringSplitOnDo = rawData.Split("do()");

        List<string> viableSegments = new();
        foreach (string splitString in stringSplitOnDo)
        {
            if (splitString.Contains("don't()"))
                viableSegments.Add(splitString.Split("don't()")[0]);
            else
                viableSegments.Add(splitString);
        }

        string filteredString = String.Join("", viableSegments);
        /* Above code not needed for Part 1 solution */


        var trimmedMatches = Regex.Matches(filteredString, @"mul\(\d+,\d+\)")
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

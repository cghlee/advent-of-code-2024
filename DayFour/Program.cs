using System.Text.RegularExpressions;
using Utilities.IO;

namespace DayFour;

internal class Program
{
    static void Main(string[] args)
    {
        string rawData = FileUtilities.GetRawData("input.txt");


        /*  SOLUTION FOR PART 1
        string paddedData = MatchHelper.GetPaddedData(rawData);

        string[] splitData = paddedData.Split();

        int totalXmasCount = 0;
        for (int i = 0; i < splitData.Length - 6; i++)
        {
            string[] dataSegment = splitData[i .. (i + 7)];

            totalXmasCount += MatchHelper.GetXmasMatches(dataSegment);
        }

        Console.WriteLine(totalXmasCount);
            SOLUTION FOR PART 1 */


        string[] splitData = rawData.Split();

        int totalCrossMatches = 0;
        for (int i = 0; i < splitData.Length - 2; i++)
        {
            for (int j = 0; j < splitData[0].Length - 2; j++)
            {
                string[] threeByThree = new string[]
                    {
                        splitData[i][j .. (j + 3)],
                        splitData[i + 1][j .. (j + 3)],
                        splitData[i + 2][j .. (j + 3)],
                    };

                string flattened = String.Join("", threeByThree);

                string msPattern = @"M.S.A.M.S";
                string smPattern = @"S.M.A.S.M";
                string mmPattern = @"M.M.A.S.S";
                string ssPattern = @"S.S.A.M.M";

                if (Regex.Match(flattened, msPattern).Success || Regex.Match(flattened, smPattern).Success
                    || Regex.Match(flattened, mmPattern).Success || Regex.Match(flattened, ssPattern).Success)
                {
                    totalCrossMatches++;
                }
            }
        }

        Console.WriteLine(totalCrossMatches);
    }
}

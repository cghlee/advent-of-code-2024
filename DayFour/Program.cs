using System.Text;
using System.Text.RegularExpressions;
using Utilities.IO;

namespace DayFour;

internal class Program
{
    static void Main(string[] args)
    {
        string rawData = FileUtilities.GetRawData("input.txt");

        /*  PART ONE SOLUTION
        int lineLength = rawData.Split()[0].Length;
        string linePadding = new String('O', lineLength) + "\n";

        StringBuilder paddingBuilder = new StringBuilder();
        for (int i = 0; i <= 2; i++)
            paddingBuilder.Append(linePadding);

        StringBuilder fullBuilder = new StringBuilder();
        fullBuilder.Append(paddingBuilder)
                   .Append(rawData)
                   .Append(paddingBuilder);

        string paddedData = fullBuilder.ToString();


        string[] splitData = paddedData.Split();

        int xmasCountSum = 0;
        for (int i = 0; i < splitData.Length - 6; i++)
        {
            string[] dataSegment = splitData[i .. (i + 7)];

            xmasCountSum += GetXmasMatches(dataSegment);
        }

        Console.WriteLine(xmasCountSum);
            PART ONE SOLUTION */

        string[] splitData = rawData.Split().ToArray();

        int xmasMatch = 0;
        for (int i = 0; i < splitData.Length - 2; i++)
        {
            for (int j = 0; j < splitData[0].Length - 2; j++)
            {
                string[] threeByThree =
                    [
                        splitData[i][j .. (j + 3)],
                        splitData[i + 1][j .. (j + 3)],
                        splitData[i + 2][j .. (j + 3)],
                    ];

                string flattened = String.Join("", threeByThree);

                string msPattern = @"M.S.A.M.S";
                string smPattern = @"S.M.A.S.M";
                string mmPattern = @"M.M.A.S.S";
                string ssPattern = @"S.S.A.M.M";

                if (Regex.Match(flattened, msPattern).Success || Regex.Match(flattened, smPattern).Success
                    || Regex.Match(flattened, mmPattern).Success || Regex.Match(flattened, ssPattern).Success)
                {
                    xmasMatch++;
                }
            }
        }

        Console.WriteLine(xmasMatch);
    }

    static int GetXmasMatches(string[] fullSegment)
    {
        string[] topSegment = fullSegment[..4];
        string[] bottomSegment = fullSegment[3..];

        int xmasCounter = 0;
        for (int i = 0; i < fullSegment[0].Length; i++)
        {
            if (fullSegment[3][i] == 'X')
            {
                bool isUpLeftMatch = UpLeftMatch(topSegment, i);
                bool isUpRightMatch = UpRightMatch(topSegment, i);
                bool isUpMatch = UpVerticalMatch(topSegment, i);

                bool isDownLeftMatch = DownLeftMatch(bottomSegment, i);
                bool isDownRightMatch = DownRightMatch(bottomSegment, i);
                bool isDownMatch = DownVerticalMatch(bottomSegment, i);

                bool isLeftMatch = LeftMatch(fullSegment[3], i);
                bool isRightMatch = RightMatch(fullSegment[3], i);

                int xmasCount = new[] { isUpLeftMatch, isUpRightMatch, isUpMatch,
                                        isDownLeftMatch, isDownRightMatch, isDownMatch,
                                        isLeftMatch, isRightMatch }
                                        .Count(b => b);

                xmasCounter += xmasCount;
            }
        }

        return xmasCounter;
    }

    static bool UpLeftMatch(string[] topSegment, int i)
    {
        if (i < 3)
            return false;

        if (topSegment[2][i - 1] == 'M')
            if (topSegment[1][i - 2] == 'A')
                if (topSegment[0][i - 3] == 'S')
                    return true;

        return false;
    }

    static bool UpRightMatch(string[] topSegment, int i)
    {
        if (i > topSegment[0].Length - 4)
            return false;

        if (topSegment[2][i + 1] == 'M')
            if (topSegment[1][i + 2] == 'A')
                if (topSegment[0][i + 3] == 'S')
                    return true;

        return false;
    }

    static bool UpVerticalMatch(string[] topSegment, int i)
    {
        if (topSegment[2][i] == 'M')
            if (topSegment[1][i] == 'A')
                if (topSegment[0][i] == 'S')
                    return true;

        return false;
    }

    static bool DownLeftMatch(string[] bottomSegment, int i)
    {
        if (i < 3)
            return false;

        if (bottomSegment[1][i - 1] == 'M')
            if (bottomSegment[2][i - 2] == 'A')
                if (bottomSegment[3][i - 3] == 'S')
                    return true;

        return false;
    }

    static bool DownRightMatch(string[] bottomSegment, int i)
    {
        if (i > bottomSegment[0].Length - 4)
            return false;

        if (bottomSegment[1][i + 1] == 'M')
            if (bottomSegment[2][i + 2] == 'A')
                if (bottomSegment[3][i + 3] == 'S')
                    return true;

        return false;
    }

    static bool DownVerticalMatch(string[] bottomSegment, int i)
    {
        if (bottomSegment[1][i] == 'M')
            if (bottomSegment[2][i] == 'A')
                if (bottomSegment[3][i] == 'S')
                    return true;

        return false;
    }

    static bool LeftMatch(string line, int i)
    {
        if (i < 3)
            return false;

        if (line[i - 1] == 'M')
            if (line[i - 2] == 'A')
                if (line[i - 3] == 'S')
                    return true;

        return false;
    }

    static bool RightMatch(string line, int i)
    {
        if (i > line.Length - 4)
            return false;

        if (line[i + 1] == 'M')
            if (line[i + 2] == 'A')
                if (line[i + 3] == 'S')
                    return true;

        return false;
    }
}

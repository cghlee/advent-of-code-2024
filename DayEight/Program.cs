using Utilities.IO;

namespace DayEight;

internal class Program
{
    static void Main()
    {
        string rawData = FileUtilities.GetRawData("input.txt");
        char[][] splitRows = rawData.Split().Select(r => r.ToCharArray()).ToArray();

        int width = splitRows[0].Length;
        int height = splitRows.Length;

        Dictionary<char, List<(int, int)>> allCoords = new();

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                char currentChar = splitRows[i][j];

                if (currentChar == '.')
                    continue;

                if (!allCoords.ContainsKey(currentChar))
                    allCoords[currentChar] = new List<(int, int)> { (i, j) };
                else
                    allCoords[currentChar].Add((i, j));
            }
        }

        foreach (var kVP in allCoords)
        {
            List<(int, int)> currentCoords = kVP.Value;

            for (int i = 0; i < currentCoords.Count; i++)
            {
                for (int j = i + 1; j < currentCoords.Count; j++)
                {
                    int x1 = currentCoords[i].Item1;
                    int y1 = currentCoords[i].Item2;

                    int x2 = currentCoords[j].Item1;
                    int y2 = currentCoords[j].Item2;

                    int xDiff = x2 - x1;
                    int yDiff = y2 - y1;

                    int antiNodeOneX = x2 + xDiff;
                    int antiNodeOneY = y2 + yDiff;

                    while (antiNodeOneX >= 0 && antiNodeOneX < height
                           && antiNodeOneY >= 0 && antiNodeOneY < width)
                    {
                        splitRows[antiNodeOneX][antiNodeOneY] = 'X';
                        antiNodeOneX += xDiff;
                        antiNodeOneY += yDiff;
                    }

                    int antiNodeTwoX = x1 - xDiff;
                    int antiNodeTwoY = y1 - yDiff;

                    while (antiNodeTwoX >= 0 && antiNodeTwoX < height
                           && antiNodeTwoY >= 0 && antiNodeTwoY < width)
                    {
                        splitRows[antiNodeTwoX][antiNodeTwoY] = 'X';
                        antiNodeTwoX -= xDiff;
                        antiNodeTwoY -= yDiff;
                    }
                }
            }
        }

        int antiNodeCount = 0;
        foreach (char[] row in splitRows)
            foreach (char c in row)
                if (c != '.')
                    antiNodeCount++;

        Console.WriteLine(antiNodeCount);
    }
}

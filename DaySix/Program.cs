using Utilities.IO;

namespace DaySix;

internal class Program
{
    static void Main()
    {
        string rawData = FileUtilities.GetRawData("input.txt");

        char[][] splitRows = rawData.Split("\n")
                                    .Select(r => r.ToCharArray())
                                    .ToArray();
        int rowLength = splitRows[0].Length;

        int turnCounter = 0;

        bool isOutOfBounds = false;
        while (!isOutOfBounds)
        {
            for (int i = 0; i < splitRows.Length; i++)
            {
                for (int j = 0; j < rowLength; j++)
                {
                    if ("<^v>".Contains(splitRows[i][j]))
                    {
                        Console.WriteLine(++turnCounter);

                        switch (splitRows[i][j])
                        {
                            case '^':
                                if (i - 1 < 0)
                                {
                                    splitRows[i][j] = 'X';
                                    isOutOfBounds = true;
                                    break;
                                }

                                if (splitRows[i - 1][j] == '#')
                                {
                                    splitRows[i][j] = '>';
                                }
                                else
                                {
                                    splitRows[i - 1][j] = '^';
                                    splitRows[i][j] = 'X';
                                }
                                break;

                            case '>':
                                if (j + 1 >= rowLength)
                                {
                                    splitRows[i][j] = 'X';
                                    isOutOfBounds = true;
                                    break;
                                }

                                if (splitRows[i][j + 1] == '#')
                                {
                                    splitRows[i][j] = 'v';
                                }
                                else
                                {
                                    splitRows[i][j + 1] = '>';
                                    splitRows[i][j] = 'X';
                                }
                                break;
                            case '<':
                                if (j - 1 < 0)
                                {
                                    splitRows[i][j] = 'X';
                                    isOutOfBounds = true;
                                    break;
                                }

                                if (splitRows[i][j - 1] == '#')
                                {
                                    splitRows[i][j] = '^';
                                }
                                else
                                {
                                    splitRows[i][j - 1] = '<';
                                    splitRows[i][j] = 'X';
                                }
                                break;
                            case 'v':
                                if (i + 1 >= splitRows.Length)
                                {
                                    splitRows[i][j] = 'X';
                                    isOutOfBounds = true;
                                    break;
                                }

                                if (splitRows[i + 1][j] == '#')
                                {
                                    splitRows[i][j] = '<';
                                }
                                else
                                {
                                    splitRows[i + 1][j] = 'v';
                                    splitRows[i][j] = 'X';
                                }
                                break;
                        }
                    }
                }
            }
        }

        string collapsedRows = String.Join("", splitRows.Select(r => new string(r)));
        int sumX = collapsedRows.Count(c => c == 'X');
        Console.WriteLine(sumX);



        List<(int, int)> pathCoords = new();

        for (int i = 0; i < splitRows.Length; i++)
        {
            for (int j = 0; j < rowLength; j++)
            {
                if (splitRows[i][j] == 'X')
                {
                    pathCoords.Add((i, j));
                }
            }
        }

        Console.WriteLine(pathCoords.Count);



        /* PART 2 SOLUTION BELOW */

        int infiniteLoopCounter = 0;
        var lockObject = new object();

        Parallel.ForEach(pathCoords, coord =>
        {
            char[][] splitRows2 = rawData.Split("\n")
                            .Select(r => r.ToCharArray())
                            .ToArray();

            splitRows2[coord.Item1][coord.Item2] = '#';

            bool isInfiniteLoop = false;
            int turnCounter2 = 0;
            bool isOutOfBounds2 = false;
            while (!isOutOfBounds2)
            {
                if (coord.Item1 == 75 && coord.Item2 == 73)
                    break;

                for (int i = 0; i < splitRows2.Length; i++)
                {
                    if (isInfiniteLoop)
                        break;

                    for (int j = 0; j < rowLength; j++)
                    {
                        if ("<^v>".Contains(splitRows2[i][j]))
                        {
                            turnCounter2++;
                            if (turnCounter2 > 10000)
                            {
                                isInfiniteLoop = true;
                                break;
                            }

                            switch (splitRows2[i][j])
                            {
                                case '^':
                                    if (i - 1 < 0)
                                    {
                                        splitRows2[i][j] = 'X';
                                        isOutOfBounds2 = true;
                                        break;
                                    }

                                    if (splitRows2[i - 1][j] == '#')
                                    {
                                        splitRows2[i][j] = '>';
                                    }
                                    else
                                    {
                                        splitRows2[i - 1][j] = '^';
                                        splitRows2[i][j] = 'X';
                                    }
                                    break;

                                case '>':
                                    if (j + 1 >= rowLength)
                                    {
                                        splitRows2[i][j] = 'X';
                                        isOutOfBounds2 = true;
                                        break;
                                    }

                                    if (splitRows2[i][j + 1] == '#')
                                    {
                                        splitRows2[i][j] = 'v';
                                    }
                                    else
                                    {
                                        splitRows2[i][j + 1] = '>';
                                        splitRows2[i][j] = 'X';
                                    }
                                    break;
                                case '<':
                                    if (j - 1 < 0)
                                    {
                                        splitRows2[i][j] = 'X';
                                        isOutOfBounds2 = true;
                                        break;
                                    }

                                    if (splitRows2[i][j - 1] == '#')
                                    {
                                        splitRows2[i][j] = '^';
                                    }
                                    else
                                    {
                                        splitRows2[i][j - 1] = '<';
                                        splitRows2[i][j] = 'X';
                                    }
                                    break;
                                case 'v':
                                    if (i + 1 >= splitRows2.Length)
                                    {
                                        splitRows2[i][j] = 'X';
                                        isOutOfBounds2 = true;
                                        break;
                                    }

                                    if (splitRows2[i + 1][j] == '#')
                                    {
                                        splitRows2[i][j] = '<';
                                    }
                                    else
                                    {
                                        splitRows2[i + 1][j] = 'v';
                                        splitRows2[i][j] = 'X';
                                    }
                                    break;
                            }
                        }
                    }
                }

                if (isInfiniteLoop)
                {
                    lock (lockObject)
                    {
                        infiniteLoopCounter++;
                        break;
                    }
                }
            }
        });

        Console.WriteLine(infiniteLoopCounter);
    }
}

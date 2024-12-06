using Utilities.IO;

namespace DaySix;

internal class Program
{
    static void Main(string[] args)
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
    }
}

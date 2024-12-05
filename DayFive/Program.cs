using Utilities.IO;

namespace DayFive;

internal class Program
{
    static void Main()
    {
        string rawData = FileUtilities.GetRawData("input.txt");

        string[] separatedData = rawData.Split("\n\n");

        string[] orderingRules = separatedData[0].Split("\n");
        string[] allUpdatesRows = separatedData[1].Split("\n");

        List<string[]> correctRows = new();

        for (int i = 0; i < allUpdatesRows.Length; i++)
        {
            string[] currentRow = allUpdatesRows[i].Split(',');

            bool isCorrectRow = true;
            for (int j = 0; j < currentRow.Length; j++)
            {
                string[] rowSegment = currentRow[..(j + 1)];

                foreach (string rule in orderingRules)
                {
                    string[] splitRule = rule.Split('|');
                    string ruleValue = splitRule[0];
                    string valueToBeAfter = splitRule[1];

                    if (rowSegment[j] == ruleValue)
                        if (rowSegment.Contains(valueToBeAfter))
                        {
                            isCorrectRow = false;
                            break;
                        }
                }

                if (!isCorrectRow)
                    break;
            }

            if (isCorrectRow)
                correctRows.Add(currentRow);
        }

        int sumMiddle = 0;
        foreach (string[] row in correctRows)
        {
            int middleIndex = (row.Length - 1) / 2;

            sumMiddle += int.Parse(row[middleIndex]);
        }

        Console.WriteLine(sumMiddle);
    }
}

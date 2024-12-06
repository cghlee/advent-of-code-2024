using Utilities.IO;

namespace DayFive;

internal class Program
{
    static void Main()
    {
        string rawData = FileUtilities.GetRawData("input.txt");

        string[] separatedData = rawData.Split("\n\n");

        string[] orderingRules = separatedData[0].Split("\n");
        string[][] splitRules = orderingRules.Select(r => r.Split('|')).ToArray();

        string[] allUpdates = separatedData[1].Split("\n");

        List<string[]> orderedRows = new();
        List<string[]> nonOrderedRows = new();

        for (int i = 0; i < allUpdates.Length; i++)
        {
            string[] currentRow = allUpdates[i].Split(',');

            bool isCorrectRow = true;
            for (int j = 0; j < currentRow.Length; j++)
            {
                string[] precedingRowSegment = currentRow[..(j + 1)];

                foreach (string[] rule in splitRules)
                {
                    string ruleValue = rule[0];
                    string valueToBeAfter = rule[1];

                    if (precedingRowSegment[j] == ruleValue)
                    {
                        if (precedingRowSegment.Contains(valueToBeAfter))
                        {
                            isCorrectRow = false;
                            break;
                        }
                    }
                }

                if (!isCorrectRow)
                    break;
            }

            if (isCorrectRow)
                orderedRows.Add(currentRow);
            else
                nonOrderedRows.Add(currentRow);
        }

        int partOneSumMiddle = SumMiddleNumbers(orderedRows);
        Console.WriteLine("Part 1 answer: " + partOneSumMiddle);


        List<string[]> partTwoCorrectedRows = new();

        for (int i = 0; i < nonOrderedRows.Count; i++)
        {
            List<string> currentRow = nonOrderedRows[i].ToList();

            while (true)
            {
                bool isFullyOrdered = true;

                for (int j = 0; j < currentRow.Count; j++)
                {
                    List<string> precedingRowSegment = currentRow[..(j + 1)];

                    foreach (string[] rule in splitRules)
                    {
                        string ruleValue = rule[0];
                        string valueToBeAfter = rule[1];

                        if (precedingRowSegment[j] == ruleValue)
                        {
                            if (precedingRowSegment.Contains(valueToBeAfter))
                            {
                                int wrongElementIndex = currentRow.IndexOf(valueToBeAfter);
                                currentRow.RemoveAt(wrongElementIndex);
                                currentRow.Add(valueToBeAfter);

                                isFullyOrdered = false;
                            }
                        }
                    }
                }

                if (isFullyOrdered)
                {
                    partTwoCorrectedRows.Add(currentRow.ToArray());
                    break;
                }
            }
        }

        int partTwoSumMiddle = SumMiddleNumbers(partTwoCorrectedRows);
        Console.WriteLine("Part 2 answer: " + partTwoSumMiddle);
    }

    static int SumMiddleNumbers(List<string[]> orderedRows)
    {
        int totalSum = 0;
        foreach (string[] row in orderedRows)
        {
            int middleIndex = (row.Length - 1) / 2;
            totalSum += int.Parse(row[middleIndex]);
        }
        return totalSum;
    }
}

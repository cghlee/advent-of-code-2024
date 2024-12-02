namespace DayTwo;

internal class Program
{
    static void Main(string[] args)
    {
        string baseDirectory = Path.GetDirectoryName(Environment.ProcessPath)!;
        string inputFilePath = Path.Combine(baseDirectory, "input.txt");

        string rawData;
        using (FileStream fileStream = new(inputFilePath, FileMode.Open, FileAccess.Read))
        {
            StreamReader sr = new StreamReader(fileStream);
            rawData = sr.ReadToEnd();
        }

        int safeReportCount = 0;
        string[] allRows = rawData.Split("\n");
        foreach (string row in allRows)
        {
            if (row.Length == 0)
                continue;

            string[] splitRow = row.Split(' ');
            int[] splitIntRow = splitRow.Where(s => s.Length > 0)
                                        .Select(s => int.Parse(s))
                                        .ToArray();

            int[] intRowAscending = splitIntRow.Order()
                                               .ToArray();
            int[] intRowDescending = splitIntRow.OrderDescending()
                                                .ToArray();

            bool isRowAscOrDesc = splitIntRow.SequenceEqual(intRowAscending)
                                  || splitIntRow.SequenceEqual(intRowDescending);


            if (!isRowAscOrDesc)
                continue;

            bool isGradualDiff = true;
            for (int i = 1; i < splitIntRow.Length; i++)
            {
                int levelDiff = Math.Abs(splitIntRow[i] - splitIntRow[i - 1]);

                if (levelDiff is >3 or 0)
                {
                    isGradualDiff = false;
                    break;
                } 
            }

            if (isGradualDiff)
            {
                safeReportCount++;
            }
        }

        Console.WriteLine(safeReportCount);
    }
}

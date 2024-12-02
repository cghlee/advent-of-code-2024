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

            List<List<int>> allPossibleReports = new List<List<int>>();

            bool isSafeReport = ReportHelper.SafeReportChecker(splitIntRow);

            if (!isSafeReport)
            {
                List<List<int>> altReports = ReportHelper.GetAltReports(splitIntRow);

                foreach (List<int> report in altReports)
                {
                    isSafeReport = ReportHelper.SafeReportChecker(report.ToArray());

                    if (isSafeReport)
                    {
                        isSafeReport = true;
                        break;
                    }
                }
            }

            if (isSafeReport)
            {
                safeReportCount++;
            }
        }

        Console.WriteLine(safeReportCount);
    }
}

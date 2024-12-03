using Utilities.IO;

namespace DayTwo;

internal class Program
{
    static void Main(string[] args)
    {
        string rawData = FileUtilities.GetRawData("input.txt");

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

            bool isSafeReport = ReportHelper.SafeReportChecker(splitIntRow);

            // Following if block not used for Part 1 solution
            if (!isSafeReport)
            {
                List<List<int>> altReports = ReportHelper.GetAltReports(splitIntRow);

                foreach (List<int> report in altReports)
                {
                    isSafeReport = ReportHelper.SafeReportChecker(report.ToArray());
                    if (isSafeReport)
                        break;
                }
            }

            if (isSafeReport)
                safeReportCount++;
        }

        Console.WriteLine(safeReportCount);
    }
}

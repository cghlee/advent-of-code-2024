namespace DayTwo;

internal static class ReportHelper
{
    internal static List<List<int>> GetAltReports(int[] report)
    {
        List<List<int>> allAltReports = new List<List<int>>();

        for (int i = 0; i < report.Length; i++)
        {
            List<int> altReport = report.ToList();
            altReport.RemoveAt(i);

            allAltReports.Add(altReport);
        }

        return allAltReports;
    }

    internal static bool SafeReportChecker(int[] report)
    {
        return AscDescChecker(report)
               && GradualDiffChecker(report);
    }

    private static bool AscDescChecker(int[] report)
    {
        int[] reportAsc = GetAscendingReport(report);
        int[] reportDesc = GetDescendingReport(report);

        bool isReportOrdered = report.SequenceEqual(reportAsc)
                               || report.SequenceEqual(reportDesc);

        if (!isReportOrdered)
            return false;

        return true;
    }

    private static bool GradualDiffChecker(int[] report)
    {
        bool isGradualDiff = true;
        for (int i = 1; i < report.Length; i++)
        {
            int levelDiff = Math.Abs(report[i] - report[i - 1]);

            if (levelDiff is > 3 or 0)
            {
                isGradualDiff = false;
                break;
            }
        }

        if (!isGradualDiff)
            return false;

        return true;
    }

    private static int[] GetAscendingReport(int[] report)
    {
        int[] reportAsc = report.Order()
                                .ToArray();
        return reportAsc;
    }

    private static int[] GetDescendingReport(int[] report)
    {
        int[] reportDesc = report.OrderDescending()
                                 .ToArray();
        return reportDesc;
    }
}

using Utilities.IO;

namespace DaySeven;

internal class Program
{
    static void Main()
    {
        string rawData = FileUtilities.GetRawData("input.txt");

        long currentTotal = 0;

        foreach (string row in rawData.Split('\n'))
        {
            string[] splitRow = row.Split(": ");

            long testValue = long.Parse(splitRow[0]);
            int[] numbers = Array.ConvertAll(splitRow[1].Split(' '), int.Parse);

            if(CanReachTestValue(testValue, numbers))
                currentTotal += testValue;
        }

        Console.WriteLine(currentTotal);
    }

    static bool CanReachTestValue(long testValue, int[] numbers)
    {
        return CalculateTotal(testValue, numbers, 0, numbers[0]);
    }

    static bool CalculateTotal(long testValue, int[] numbers, int currentIndex, long rollingSum)
    {
        if (currentIndex == numbers.Length - 1)
        {
            bool isTestValueReached = (rollingSum == testValue);
            return isTestValueReached;
        }

        long nextNumber = numbers[currentIndex + 1];
        long concatSum = long.Parse($"{rollingSum}{nextNumber}");

        return CalculateTotal(testValue, numbers, currentIndex + 1, rollingSum + nextNumber) ||
               CalculateTotal(testValue, numbers, currentIndex + 1, rollingSum * nextNumber) ||
               // Comment out below line for Part 1 solution
               CalculateTotal(testValue, numbers, currentIndex + 1, concatSum);
    }
}

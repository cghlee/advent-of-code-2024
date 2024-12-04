using Utilities.IO;

namespace DayFour;

internal class Program
{
    static void Main(string[] args)
    {
        string rawData = FileUtilities.GetRawData("input.txt");

        Console.WriteLine(rawData.Split("\n").Length);
    }
}

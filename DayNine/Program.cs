using System.Diagnostics.Metrics;
using System.Text;
using Utilities.IO;

namespace DayNine;

internal class Program
{
    static void Main()
    {
        string rawData = FileUtilities.GetRawData("input.txt");
        //string rawData = "2333133121414131402";

        List<string> listValues = new List<string>();

        int currentId = 0;
        for (int i = 0; i < rawData.Length; i++)
        {
            if (i % 2 == 0)
            {
                for (int j = 0; j < int.Parse(rawData[i].ToString()); j++)
                    listValues.Add(currentId.ToString());

                currentId++;
            }
            else
            {
                for (int k = 0; k < int.Parse(rawData[i].ToString()); k++)
                    listValues.Add(".");
            }
        }

        while (true)
        {
            int firstDotIndex = listValues.FindIndex(v => v == ".");
            int lastNumberIndex = listValues.FindLastIndex(v => v != ".");

            if (firstDotIndex >= lastNumberIndex)
                break;

            string lastNumber = listValues[lastNumberIndex];

            listValues[firstDotIndex] = lastNumber;
            listValues[lastNumberIndex] = ".";
        }

        long rollingSum = 0;
        int currentIndex = 0;
        while (listValues[currentIndex] != ".")
        {
            int currentInt = int.Parse(listValues[currentIndex]);
            rollingSum += (currentInt * currentIndex);

            currentIndex++;
        }

        Console.WriteLine(rollingSum);
    }
}

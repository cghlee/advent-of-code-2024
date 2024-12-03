namespace Utilities.IO;

public static class FileUtilities
{
    public static string GetRawData(string fileName)
    {
        string baseDirectory = Path.GetDirectoryName(Environment.ProcessPath)!;
        string inputFilePath = Path.Combine(baseDirectory, fileName);

        string data;
        using (FileStream fileStream = new(inputFilePath, FileMode.Open, FileAccess.Read))
        {
            StreamReader sr = new StreamReader(fileStream);
            data = sr.ReadToEnd();
        }

        return data;
    }
}

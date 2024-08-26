using Domain.Interfaces.Repositories;

namespace Infrastructure.Repositories;

public class ArrayTextFileRepository : IArrayRepository
{
    private const string DataDirectoryPath = ".";
    private const string TextFilePattern = @"*.txt";

    public async Task SaveArrayAsync(int[] array)
    {
        var outputNumberLine = string.Join(' ', array);

        File.WriteAllText(DataDirectoryPath + "\result.txt", outputNumberLine);        
    }

    public async Task<string> GetLatestArrayAsync()
    {
        var directory = new DirectoryInfo(DataDirectoryPath);

        var latestFile = directory.GetFiles(TextFilePattern)
            .OrderByDescending(f => f.LastWriteTime)
            .First();

        var latestFileContents = File.ReadAllText(latestFile.FullName);

        return latestFileContents;
    }
}

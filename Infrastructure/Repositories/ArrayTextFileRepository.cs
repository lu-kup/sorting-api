using Domain.Interfaces.Repositories;
using Application.Utilities;

namespace Infrastructure.Repositories;

public class ArrayTextFileRepository : IArrayRepository
{
    private const string DataDirectory = "Data";
    private const string InfrastructureDirectory = "Infrastructure";
    private const string TextFilePattern = @"*.txt";
    private const string DirectoryMissingMessage = "Directory at the provided data output path does not exist.";

    private string _dataDirectoryPath;

    public ArrayTextFileRepository()
    {
        _dataDirectoryPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            InfrastructureDirectory,
            DataDirectory);

        if (!Directory.Exists(_dataDirectoryPath))
        {
            throw new DirectoryNotFoundException(DirectoryMissingMessage);
        }
    }

    public async Task SaveAsync(int[] array)
    {
        var arrayData = ArraySerializationUtility.Serialize(array);

        var filePath = Path.Combine(_dataDirectoryPath, GenerateFilename());

        File.WriteAllText(filePath, arrayData);        
    }

    public async Task<string> GetLatestAsync()
    {
        var directory = new DirectoryInfo(_dataDirectoryPath);

        var latestFile = directory.GetFiles(TextFilePattern)
            .OrderByDescending(f => f.LastWriteTime)
            .First();

        var latestFileContents = File.ReadAllText(latestFile.FullName);

        return latestFileContents;
    }

    private string GenerateFilename() =>
        $"Result_{DateTime.UtcNow.ToString("MM-dd_HH:mm:ss")}.txt";
}

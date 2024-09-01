using Domain.Interfaces.Repositories;
using Application.Utilities;

namespace Infrastructure.Repositories;

public class ArrayTextFileRepository : IArrayRepository
{
    public const string DataDirectory = "Data";
    public const string TextFilePattern = @"*.txt";
    private const string InfrastructureDirectory = "Infrastructure";

    private readonly string _dataDirectoryPath;

    public ArrayTextFileRepository(string projectDirectory = InfrastructureDirectory)
    {
        var baseDirectory = Directory.GetParent(Directory.GetCurrentDirectory());

        _dataDirectoryPath = Path.Combine(
            baseDirectory?.FullName ?? ".",
            projectDirectory,
            DataDirectory);

        if (!Directory.Exists(_dataDirectoryPath))
        {
            throw new DirectoryNotFoundException(GetDirectoryMissingMessage());
        }
    }

    public async Task SaveAsync(int[] array)
    {
        var arrayData = ArraySerializationUtility.Serialize(array);

        var filePath = Path.Combine(_dataDirectoryPath, GenerateFilename());

        await File.WriteAllTextAsync(filePath, arrayData);
    }

    public async Task<string?> GetLatestAsync()
    {
        var directory = new DirectoryInfo(_dataDirectoryPath);

        var latestFile = directory.GetFiles(TextFilePattern)
            .OrderByDescending(f => f.LastWriteTime)
            .FirstOrDefault();

        var latestFileContents = latestFile is null ?
            null : await File.ReadAllTextAsync(latestFile.FullName);

        return latestFileContents;
    }

    private string GenerateFilename() =>
        $"Result_{DateTime.UtcNow.ToString("MM-dd_HH-mm-ss")}.txt";

    private string GetDirectoryMissingMessage() => 
        $"Directory at the provided data output path does not exist. Path: {_dataDirectoryPath}";
}

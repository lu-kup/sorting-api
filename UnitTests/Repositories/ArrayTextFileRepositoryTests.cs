using Infrastructure.Repositories;
using Domain.Interfaces.Repositories;

namespace UnitTests.Repositories;

public class ArrayTextFileRepositoryTests : IDisposable
{
    public const string TestDirectory = "../..";

    private readonly IArrayRepository _arrayTextFileRepository;
    private readonly string _testDataPath;

    public ArrayTextFileRepositoryTests()
    {
        _arrayTextFileRepository = new ArrayTextFileRepository(TestDirectory);

        var baseDirectory = Directory.GetParent(Directory.GetCurrentDirectory());

        _testDataPath = Path.Combine(
            baseDirectory?.FullName ?? ".",
            TestDirectory,
            ArrayTextFileRepository.DataDirectory);

        if (!Directory.Exists(_testDataPath))
        {
            throw new DirectoryNotFoundException("Test data directory not found.");
        }
    }

    [Theory]
    [InlineData(new int[] { 132 }, "132")]
    [InlineData(new int[] { 10, 0, 3, 3 }, "10 0 3 3")]
    [InlineData(new int[] { 1, 1, 1 }, "1 1 1")]
    public async Task SaveAsync_GivenVariousInputArrays_SavesCorrectString(
        int[] inputArray,
        string expectedSavedString)
    {
        // Arrange
        var testDataDirectory = new DirectoryInfo(_testDataPath);

        // Act
        await _arrayTextFileRepository.SaveAsync(inputArray);

        // Assert
        var savedFile = testDataDirectory.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
        var savedData = await File.ReadAllTextAsync(savedFile.FullName);

        Assert.Equal(expectedSavedString, savedData);
        Assert.Matches(@"Result_.*.txt", savedFile.Name);
    }

    [Fact]
    public async Task GetLatestAsync_GivenMultipleTextFiles_RetrievesLatestTextInput()
    {
        // Arrange
        var latestTextInput = "lastNumberLine";

        var filePath1 = Path.Combine(_testDataPath, "Result1.txt");
        await File.WriteAllTextAsync(filePath1, "numberLine1");

        var filePath2 = Path.Combine(_testDataPath, "Result2.txt");
        await File.WriteAllTextAsync(filePath2, "numberLine2");

        var filePath3 = Path.Combine(_testDataPath, "Result3.txt");
        await File.WriteAllTextAsync(filePath3, latestTextInput);

        // Act
        var retrievedString = await _arrayTextFileRepository.GetLatestAsync();

        // Assert
        Assert.Equal(retrievedString, latestTextInput);
    }

    public void Dispose()
    {
        Directory.GetFiles(_testDataPath)
            .ToList()
            .ForEach(x => File.Delete(x));
    }
}

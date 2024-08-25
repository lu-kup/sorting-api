using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Repositories;

public class ArrayTextFileRepository : IArrayRepository
{
    private readonly StreamWriter _writer = new("result.txt");

    public async Task SaveArrayAsync(int[] array) =>
        await _writer.WriteLineAsync(string.Join(" ", array));

    public async Task<int[]> GetLatestArrayAsync()
    {
        throw new NotImplementedException();
    }
}

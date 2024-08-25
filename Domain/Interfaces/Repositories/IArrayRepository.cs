namespace Domain.Interfaces.Repositories;

public interface IArrayRepository
{
    Task SaveArrayAsync(int[] array);
    Task<int[]> GetLatestArrayAsync();
}

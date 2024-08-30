namespace Domain.Interfaces.Repositories;

public interface IArrayRepository
{
    Task SaveAsync(int[] array);
    Task<string> GetLatestAsync();
}

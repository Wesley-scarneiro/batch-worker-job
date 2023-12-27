namespace Batch.Repository.Interface;
public interface IDbContext
{
    public Task<int> Create<T>(IEnumerable<T> records);
    public Task Read<T>(T model);
    public Task Update<T>(IEnumerable<T> records);
    public Task Delete<T>(IEnumerable<T> records);
}

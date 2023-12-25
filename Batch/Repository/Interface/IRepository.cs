namespace Batch.Repository.Interface;
public interface IRepository
{
    public Task Create<T>(IEnumerable<T> records);
    public Task Read<T>(T model);
    public Task Update<T>(IEnumerable<T> records);
    public Task Delete<T>(IEnumerable<T> records);
}

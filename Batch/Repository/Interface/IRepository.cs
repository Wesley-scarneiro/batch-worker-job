namespace Batch.Repositories.Interface;
public interface IRepository
{
    public Task<int> Create<T>(IEnumerable<T> records);
    public Task<IEnumerable<T>> Read<T>();
    public Task<int> Update<T>(IEnumerable<T> records);
    public Task<int> Delete<T>(IEnumerable<T> records);
}


using Batch.Repositories.Interface;
using Dapper;

namespace Batch.Repositories;
public class Repository : IRepository
{
    private readonly DbSession _session;

    public Repository(DbSession session)
    {
        _session = session;
    }
    
    public async Task<int> Create<T>(IEnumerable<T> records)
    {
        var response = await _session.Connection.ExecuteAsync(SqlQuery.Insert<T>(), records, _session.Transaction);
        return response;
    }

    public async Task<int> Delete<T>(IEnumerable<T> records)
    {
        var response = await _session.Connection.ExecuteAsync(SqlQuery.Delete<T>(), records, _session.Transaction);
        return response;
    }

    public async Task<IEnumerable<T>> Read<T>()
    {
        var response = await _session.Connection.QueryAsync<T>(SqlQuery.Select<T>());
        return response;
    }

    public async Task<int> Update<T>(IEnumerable<T> records)
    {
        var response = await _session.Connection.ExecuteAsync(SqlQuery.Update<T>(), records, _session.Transaction);
        return response;
    }
}


using Batch.Repository.Interface;
using Dapper;

namespace Batch.Repository;
public class DbContext : IDbContext
{
    private readonly DbSession _session;

    public DbContext(DbSession session)
    {
        _session = session;
    }
    
    public async Task<int> Create<T>(IEnumerable<T> records)
    {
        try
        {
            _session.BeginTransaction();
            var response = await _session.Connection.ExecuteAsync(SqlQuery.Insert<T>(), records);
            _session.Commit();
            return response;
        }
        catch (Exception)
        {
            _session.Rollback();
            throw;
        }
    }

    public Task Delete<T>(IEnumerable<T> records)
    {
        throw new NotImplementedException();
    }

    public Task Read<T>(T model)
    {
        throw new NotImplementedException();
    }

    public Task Update<T>(IEnumerable<T> records)
    {
        throw new NotImplementedException();
    }
}


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

    public async Task<int> Delete<T>(IEnumerable<T> records)
    {
        try
        {
            _session.BeginTransaction();
            var response = await _session.Connection.ExecuteAsync(SqlQuery.Delete<T>(), records);
            _session.Commit();
            return response;
        }
        catch (Exception)
        {
            _session.Rollback();
            throw;
        }
    }

    public async Task<IEnumerable<T>> Read<T>()
    {
        try
        {
            _session.BeginTransaction();
            var response = await _session.Connection.QueryAsync<T>(SqlQuery.Select<T>());
            _session.Commit();
            return response;
        }
        catch (Exception)
        {
            _session.Rollback();
            throw;
        }
    }

    public async Task<int> Update<T>(IEnumerable<T> records)
    {
        try
        {
            _session.BeginTransaction();
            var response = await _session.Connection.ExecuteAsync(SqlQuery.Update<T>(), records);
            _session.Commit();
            return response;
        }
        catch (Exception)
        {
            _session.Rollback();
            throw;
        }
    }
}

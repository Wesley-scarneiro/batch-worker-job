using System.Data;
using Batch.Repository.Interface;
using Microsoft.Data.Sqlite;

namespace Batch.Repository;
public sealed class DbSession : IDisposable, IDbSession
{
    public IDbConnection Connection {get;}
    private IDbTransaction? _transaction;

    public DbSession(string? connectionString)
    {
        Connection = new SqliteConnection(connectionString);
        Connection.Open();
    }
    public void BeginTransaction()
    {
        _transaction = Connection?.BeginTransaction();
    }

    public void Commit()
    {
        _transaction?.Commit();
        _transaction?.Dispose();
    }

    public void Rollback()
    {
        _transaction?.Rollback();
        _transaction?.Dispose();
    }

    public void Dispose() => Connection?.Dispose();
}

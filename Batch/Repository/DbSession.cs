using System.Data;
using Microsoft.Data.Sqlite;

namespace Batch.Repositories;
public sealed class DbSession : IDisposable
{
    public IDbConnection Connection {get;}
    public IDbTransaction? Transaction {get; set;}

    public DbSession(string? connectionString)
    {
        Connection = new SqliteConnection(connectionString);
        Connection.Open();
    }

    public void Dispose() => Connection?.Dispose();
}

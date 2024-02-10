using Bath.Repository.Interface;

namespace Batch.Repositories.Interface;
public class UnitOfWork : IUnitOfWork
{
    private readonly DbSession _session;
    private readonly IRepository _repository;

    public UnitOfWork(DbSession session, IRepository repository)
    {
        _session = session;
        _repository = repository;
    }

    public void BeginTransaction()
    {
        _session.Transaction = _session.Connection.BeginTransaction();
    }

    public void Commit()
    {
        _session.Transaction?.Commit();
        Disposable();
    }

    public void Rollback()
    {
        _session.Transaction?.Rollback();
        Disposable();
    }

    public void Disposable()
    {
        _session.Transaction?.Dispose();
    }
}

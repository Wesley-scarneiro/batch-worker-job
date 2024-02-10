namespace Bath.Repository.Interface;

public interface IUnitOfWork
{
    public void BeginTransaction();
    public void Commit();
    public void Rollback();
    public void Disposable();
}

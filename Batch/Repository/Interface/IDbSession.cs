namespace Batch.Repository.Interface;

public interface IDbSession
{
    public void BeginTransaction();
    public void Commit();
    public void Rollback();
    public void Dispose();
}
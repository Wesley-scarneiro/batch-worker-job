using Batch.Repository.Interface;

namespace Batch.Repository;
public class MarketRepository : IRepository
{
    private readonly IDbContext _context;

    public MarketRepository(IDbContext context)
    {
        _context = context;
    }
    
    public async Task Create<T>(IEnumerable<T> records)
    {
        await _context.Create(records);
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

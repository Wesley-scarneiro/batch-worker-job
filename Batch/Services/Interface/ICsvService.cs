namespace Batch.Services.Interface;
public interface ICsvService
{
    public IEnumerable<T> ReadFileCsv<T>(MemoryStream mstream);
    public Task<byte[]> WriteRecords<T>(IEnumerable<T> records);
}

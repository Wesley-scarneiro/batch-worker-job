namespace Batch.Services.Interface;
public interface ICsvService
{
    public IEnumerable<T> ReadFileCsv<T>(Stream stream);
    public Task<MemoryStream> WriteRecords<T>(IEnumerable<T> records);
}

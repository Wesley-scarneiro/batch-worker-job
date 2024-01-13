using System.Globalization;
using Batch.Services.Interface;
using CsvHelper;

namespace Batch.Services;
public class CsvService : ICsvService
{
    public IEnumerable<T> ReadFileCsv<T>(Stream stream)
    {
        using var streamReader = new StreamReader(stream);
        using var csvHelper = new CsvReader(streamReader, CultureInfo.InvariantCulture);
        var records = csvHelper.GetRecords<T>();
        return records;
    }

    public async Task<MemoryStream> WriteRecords<T>(IEnumerable<T> records)
    {
        using var memoryStream = new MemoryStream();
        using var writer = new StreamWriter(memoryStream);
        using var csvHelper = new CsvWriter(writer, CultureInfo.InvariantCulture);
        await csvHelper.WriteRecordsAsync(records);
        return memoryStream;
    }
}

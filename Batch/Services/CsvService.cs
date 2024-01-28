using System.Globalization;
using Batch.Services.Interface;
using CsvHelper;

namespace Batch.Services;
public class CsvService : ICsvService
{
    public IEnumerable<T> ReadFileCsv<T>(MemoryStream mstream)
    {
        using var streamReader = new StreamReader(mstream);
        using var csvHelper = new CsvReader(streamReader, CultureInfo.InvariantCulture);
        var records = csvHelper.GetRecords<T>().ToList();
        mstream.Close();
        return records;
    }

    public async Task<byte[]> WriteRecords<T>(IEnumerable<T> records)
    {
        using var mstream = new MemoryStream();
        using var streamWriter = new StreamWriter(mstream);
        using var csvHelper = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
        await csvHelper.WriteRecordsAsync(records);
        await streamWriter.FlushAsync();
        return mstream.ToArray();
    }
}

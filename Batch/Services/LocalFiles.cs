using Batch.Services.Interface;

namespace Batch.Services;

/*
    LocalFiles implementa IFileService para manipulação de arquivos locais.
*/
public class LocalFiles : IFileService
{
    private readonly string _inputPath;
    private readonly string _outputPath;

    public LocalFiles(string inputPath, string outputPath)
    {
        _inputPath = inputPath;
        _outputPath = outputPath;
    }

    public async Task<IEnumerable<string>> GetFiles()
    {
        var fileNamesCsv = await Task.Run(() => Directory.GetFiles(_inputPath, "*.csv"));
        return fileNamesCsv;
    }

    public async Task<Stream> ReadFile(string path)
    {
        var bytes = await File.ReadAllBytesAsync(path);
        var memoryStream = new MemoryStream(bytes);
        return memoryStream;
    }

    public Task<bool> Movefile(string path)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CreateFile<T>(IEnumerable<T> records)
    {
        throw new NotImplementedException();
    }
}

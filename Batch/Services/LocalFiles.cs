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

    private static string PathCombine(string fileName, string path)
    {
        return Path.Combine(path, fileName);
    }
    public async Task<IEnumerable<string?>> GetFiles()
    {
        try
        {
            var fileNamesCsv = await Task.Run(() => Directory.GetFiles(_inputPath, "*.csv").
                            Select(Path.GetFileName));
            return fileNamesCsv;
        }
        catch(Exception)
        {
            throw;
        }
    }

    public async Task<Stream> ReadFile(string fileName)
    {
        try
        {
            var bytes = await File.ReadAllBytesAsync(PathCombine(fileName, _inputPath));
            var memoryStream = new MemoryStream(bytes);
            return memoryStream;
        }
        catch(Exception)
        {
            throw;
        }
    }

    public async Task<bool> Movefile(string fileName)
    {
        try
        {
            if (File.Exists(PathCombine(fileName, _outputPath)))
            {
                await Task.Run(() => Directory.Move(PathCombine(fileName, _inputPath), PathCombine(fileName, _outputPath)));
                return true;
            }
            return false;
        }
        catch(Exception)
        {
            throw;
        }
    }

    public Task<bool> CreateFile<T>(IEnumerable<T> records)
    {
        throw new NotImplementedException();
    }
}

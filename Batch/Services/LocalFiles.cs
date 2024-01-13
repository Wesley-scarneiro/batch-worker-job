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

    private static string PathCombine(string path, string fileName)
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
            var bytes = await File.ReadAllBytesAsync(PathCombine(_inputPath, fileName));
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
            if (File.Exists(PathCombine(_outputPath, fileName)))
            {
                await Task.Run(() => Directory.Move(PathCombine(fileName, _inputPath), PathCombine(_outputPath, fileName)));
                return true;
            }
            return false;
        }
        catch(Exception)
        {
            throw;
        }
    }

    public async Task<bool> CreateFile(string fileName, MemoryStream mstream)
    {
        using var fileStream = new FileStream(Path.Combine(_outputPath, fileName), FileMode.Create, FileAccess.Write);
        await Task.Run(() =>
        {
            var copyMstream = new MemoryStream(mstream.ToArray());
            copyMstream.WriteTo(fileStream);
        });
        if (File.Exists(PathCombine(_outputPath, fileName))) return true;
        return false;
    }
}

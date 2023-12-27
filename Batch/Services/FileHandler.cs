namespace Batch.Services;
public class FileHandler
{
    private readonly string _inputPath;
    private readonly string _outputPath;

    public FileHandler(string inputPath, string outputPath)
    {
        _inputPath = inputPath;
        _outputPath = outputPath;
    }

    public IEnumerable<string> GetFiles()
    {
        var fileNamesCsv = Directory.GetFiles(_inputPath, "*.csv");
        return fileNamesCsv;
    }
}

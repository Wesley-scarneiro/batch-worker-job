using Batch.Services.Interface;

namespace Batch.Services;

/*
    FileHandler faz parte da camada de aplicação,
    pois manipula um serviço de arquivos.
    Sua responsabilidade é realizar a leitura, escrita e conversão
    dos registros em objetos dos arquivos manipulados. 
*/
public class FileHandler
{
    private readonly IFileService _fileService;
    private readonly ICsvService _csvService;

    public FileHandler(IFileService fileService, ICsvService csvService)
    {
        _fileService = fileService;
        _csvService = csvService;
    }

    public async Task<IEnumerable<string?>> GetFiles()
    {
        return await _fileService.GetFiles();
    }

    public async Task<IEnumerable<T>> ReadFile<T>(string fileName)
    {
        var stream = await _fileService.ReadFile(fileName);
        return _csvService.ReadFileCsv<T>(stream);
    }

    public async Task<bool> MoveFile(string fileName)
    {
        return await _fileService.Movefile(fileName);
    }

    public async Task<bool> CreateFile<T>(IEnumerable<T> records)
    {
        var mstream = await _csvService.WriteRecords(records);
        var fileName = $"{DateTime.Now.ToString("yyyymmdd")}_{1}_{typeof(T).Name.ToLower()}.csv";
        var response = await _fileService.CreateFile(fileName, mstream);
        return response;
    }
}

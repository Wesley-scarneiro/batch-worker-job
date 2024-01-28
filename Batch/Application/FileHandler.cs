using Batch.Application.Interfaces;
using Batch.Domain.Entities;
using Batch.Domain.Enums;
using Batch.Services.Interface;

namespace Batch.Services;

/*
    FileHandler faz parte da camada de aplicação,
    pois manipula um serviço de arquivos.
    Sua responsabilidade é realizar a leitura, escrita e conversão
    dos registros em objetos dos arquivos manipulados. 
*/
public class FileHandler : IFileHandler
{
    private List<BatchFile> _files;
    private readonly IFileService _fileService;
    private readonly ICsvService _csvService;

    public FileHandler(IFileService fileService, ICsvService csvService)
    {
        _fileService = fileService;
        _csvService = csvService;
        _files = new List<BatchFile>();
    }

    public async Task<IEnumerable<BatchFile>> GetFiles(TypeProduct type, Operation operation)
    {
        if (_files.Any()) return _files.Where(f => f.Type == type && f.Operation == operation).ToList().AsReadOnly();
        var fileNames = await _fileService.GetFiles();
        foreach (var name in fileNames) _files.Add(new BatchFile(name));
        return _files.Where(f => f.Type == type && f.Operation == operation).ToList().AsReadOnly();
    }

    public async Task<IEnumerable<T>> ReadFile<T>(string fileName)
    {
        var stream = await _fileService.ReadFile(fileName);
        return _csvService.ReadFileCsv<T>((MemoryStream) stream);
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

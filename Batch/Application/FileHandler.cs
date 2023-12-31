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

    public FileHandler(IFileService fileService)
    {
        _fileService = fileService;
    }

    public async Task<IEnumerable<string>> GetFiles()
    {
        return await _fileService.GetFiles();
    }

    public async Task<Stream> ReadFile(string path)
    {
        return await _fileService.ReadFile(path); //falta converter em objetos e devolver em um enumerável
    }
}

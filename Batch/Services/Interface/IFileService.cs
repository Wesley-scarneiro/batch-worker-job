namespace Batch.Services.Interface;

/*
    Interface que define a especificação para a manipulação de arquivos.
    Com está interface, é possível implementar um serviço de arquivos locais
    ou na nuvem, por exemplo, usando AWS S3.
*/
public interface IFileService
{
    public Task<IEnumerable<string?>> GetFiles();
    public Task<Stream> ReadFile(string fileName);
    public Task<bool> Movefile(string fileName);
    public Task<bool> CreateFile(string fileName, MemoryStream stream);
}

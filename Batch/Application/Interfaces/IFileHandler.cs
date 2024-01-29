using Batch.Domain.Entities;
using Batch.Domain.Enums;

namespace Batch.Application.Interfaces;
public interface IFileHandler
{
    public Task<IEnumerable<BatchFile>> GetFiles(TypeProduct type, Operation operation);
    public Task<IEnumerable<T>> ReadFile<T>(string fileName);
    public Task<bool> MoveFile(string fileName);
    public Task<bool> CreateFile<T>(IEnumerable<T> records);
}

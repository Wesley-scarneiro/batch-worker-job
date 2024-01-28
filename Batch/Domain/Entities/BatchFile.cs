using Batch.Domain.Enums;

namespace Batch.Domain.Entities;
public record BatchFile
{
    public DateOnly Date {get; init;}
    public string Version {get; init;}
    public TypeProduct? Type {get; init;}
    public Operation? Operation {get; init;}
    public string Name {get; init;}

    public BatchFile(string fileName)
    {
        Name = fileName;
        var split = fileName.Split("_");
        Date = DateOnly.ParseExact(split[0], "yyyymmdd");
        Version = split[1];
        Type = GetProduct(split[2]);
        Operation = GetOperation(split[3].Replace(".csv", ""));
    }

    private static TypeProduct? GetProduct(string entity)
    {
        return entity switch
        {
            "products" => TypeProduct.PRODUCT,
            "suppliers" => TypeProduct.SUPPLIER,
            "delete" => TypeProduct.DELETE,
            _ => null
        };
    }

    private static Operation? GetOperation(string entity)
    {
        return entity switch
        {
            "create" => Enums.Operation.CREATE,
            "read" => Enums.Operation.READ,
            "update" => Enums.Operation.UPDATE,
            "delete" => Enums.Operation.DELETE,
            _ => null
        };
    }
}

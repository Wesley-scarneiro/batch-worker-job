namespace Batch.Domain.Entities;

public class ProductsCreate
{
    public int? ProductId {get; set;}
    public string? BarCode {get; set;}
    public string? ProductName {get; set;}
    public int? SupplierId {get; set;}
    public int? Inventory {get; set;}
    public double? Price {get; set;}
}
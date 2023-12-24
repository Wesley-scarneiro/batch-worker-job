namespace Batch.Domain.Entities;

public class SuppliersCreate
{
    public int? SupplierId {get; set;}
    public string? SupplierName {get; set;}
    public bool? ActiveContract {get; set;}
}

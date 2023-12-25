using Batch.Domain.Models;

namespace Batch.Repository;
public static class SqlQuery
{
    private static readonly string _insertProduct = @"
        INSERT INTO Products (BarCode, ProductName, SupplierId, Inventory, Price) 
        VALUES (@BarCode, @ProductName, @SupplierId, @Inventory, @Price)";
    private static readonly string _insertSupplier = @"
        INSERT INTO Suppliers (SupplierName, ActiveContract)
        VALUES (@SupplierName, @ActiveContract)";
    
    public static string Insert<T>() // tornar essa porra dinâmica!
    {
        if (typeof(T) == typeof(Products))
        {
            return _insertProduct;
        }
        else if (typeof(T) == typeof(Suppliers))
        {
            return _insertSupplier;
        }
        else
        {
            throw new InvalidOperationException($"Model '{typeof(T).Name}' does not defined for insert operation");
        }
    }
}

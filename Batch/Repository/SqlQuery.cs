namespace Batch.Repository;
public static class SqlQuery
{
    private static string _updateProduct = @"
        UPDATE Products
        SET BarCode = @BarCode,
            ProductName = @ProductName,
            SupplierId = @SupplierId,
            Inventory = @Inventory,
            Price = @Price
        WHERE ProductId = @ProductId";
    
    private static string _updateSupplier = @"
        UPDATE Suppliers
        SET SupplierName = @SupplierName,
            ActiveContract = @ActiveContract,
        WHERE SupplierId = @SupplierId";

    private static string _deleteProduct = @"
        DELETE FROM Products
        WHERE ProductId = @ProductId";
    
    private static string _deleteSupplier = @"
        DELETE Suppliers
        WHERE SupplierId = @SupplierId";
    
    public static string Insert<T>()
    {
        var type = typeof(T);
        var properties = type.GetProperties();
        string columns = string.Join(", ", properties.Select(p => p.Name));
        string parameters = string.Join(", ", properties.Select(p => "@" + p.Name));
        var query = $"INSERT INTO {type.Name + "s"} ({columns}) VALUES ({parameters})";
        return query;
    }

    public static string Select<T>()
    {
        var type = typeof(T);
        var properties = type.GetProperties();
        var columns = string.Join(", ", properties.Select(p => p.Name));
        return $"SELECT {columns} FROM {type.Name + "s"}";
    }

    public static string? Update<T>()
    {
        return typeof(T).Name switch
        {
            "Product" => _updateProduct,
            "Supplier" => _updateSupplier,
            _ => null,
        };
    }

    public static string? Delete<T>()
    {
        return typeof(T).Name switch
        {
            "DeleteProduct" => _deleteProduct,
            "DeleteSupplier" => _deleteSupplier,
            _ => null,
        };
    }
}

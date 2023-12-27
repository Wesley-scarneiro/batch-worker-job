namespace Batch.Repository;
public static class SqlQuery
{
    public static string Insert<T>()
    {
        var type = typeof(T);
        var properties = type.GetProperties();
        string columns = string.Join(", ", properties
            .Where(p => !p.Name.EndsWith(type.Name + "Id"))
            .Select(p => p.Name));
        string parameters = string.Join(", ", properties
            .Where(p => !p.Name.EndsWith(type.Name + "Id"))
            .Select(p => "@" + p.Name));
        return $"INSERT INTO {type.Name + "s"} ({columns} VALUES ({parameters}))";
    }

    public static string Select<T>()
    {
        var type = typeof(T);
        var properties = type.GetProperties();
        var columns = string.Join(", ", properties.Select(p => p.Name));
        return $"SELECT {columns} FROM {type.Name + "s"}";
    }
}

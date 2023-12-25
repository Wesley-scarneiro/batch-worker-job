using Batch.Repository;
using Batch.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Batch.Extensions;
public static class RepositoryExtensions
{
    public static IServiceCollection AddRepository(this IServiceCollection service, IConfiguration configuration)
    {
        var dbSession = new DbSession(configuration["Appsettings:ConnectionString"]);
        service.AddSingleton(dbSession);
        service.AddSingleton<IDbContext, DbContext>();
        return service;
    }
}

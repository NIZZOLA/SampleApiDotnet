using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Infra.Data.MongoDb.Context;
using System.Reflection;

namespace Store.Infra.Data.Sql;
public static class DependencyInjectionHelper
{
    public static void AddDataModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IStoreMongoDbContext,StoreMongoDbContext>();
        services.ScanDependencyInjection(Assembly.GetExecutingAssembly(), "Repository");
    }
}

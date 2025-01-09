using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Infra.Data.MongoDb.Context;
using System.Reflection;

namespace Store.Infra.Data.MongoDb;
public static class DependencyInjectionHelper
{
    public static void AddMongoDbDataModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IStoreMongoDbContext,StoreMongoDbContext>();
        services.ScanDependencyInjection(Assembly.GetExecutingAssembly(), "Repository");
        
        var mongoDbSettings = configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();

//        services.Configure<MongoDbConfiguration>(configuration.GetSection("MongoDbConfiguration"));

    }
}

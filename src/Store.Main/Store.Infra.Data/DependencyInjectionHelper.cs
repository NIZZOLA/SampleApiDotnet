using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Conventions;
using Store.Infra.Data.Configuration;
using Store.Infra.Data.Context;
using System.Reflection;

namespace Store.Infra.Data;
public static class DependencyInjectionHelper
{
    public static void AddDataModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IStoreMongoDbContext,StoreMongoDbContext>();
        services.ScanDependencyInjection(Assembly.GetExecutingAssembly(), "Repository");
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Domain;
using Store.Infra.Data.MongoDb;
using Store.Infra.Data.Sql;
using System.Reflection;

namespace Store.Business;
public static class DependencyInjectionHelper
{
    public static void AddBusiness(this IServiceCollection services, IConfiguration configuration)
    {
        services.ScanDependencyInjection(Assembly.GetExecutingAssembly(), "Service");

        if (configuration.GetSection("DatabaseType").Value == "MongoDb")
        {
            services.AddMongoDbDataModule(configuration);
        }
        else
        {
            services.AddSqlDataModule(configuration);
        }
    }
}

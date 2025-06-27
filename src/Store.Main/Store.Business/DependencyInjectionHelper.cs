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

        switch (configuration.GetSection("DatabaseType").Value)
        {
            case "MongoDb":
                services.AddMongoDbDataModule(configuration);
                break;
            case "SqlServer":
                services.AddSqlDataModule(configuration);
                break;
            case "Postgres":
            //    services.AddPostgresDataModule(configuration);
                    throw new NotImplementedException("Postgres support is not implemented yet.");
                break;
            case "MySql":
                //    services.AddMySqlDataModule(configuration);
                throw new NotImplementedException("MySql support is not implemented yet.");
                break;
            default:
                throw new InvalidOperationException($"Unsupported database type. {configuration.GetSection("DatabaseType").Value}");
        }
    }
}

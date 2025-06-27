using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Infra.Data.Sql.Context;
using System.Reflection;

namespace Store.Infra.Data.Sql;
public static class DependencyInjectionHelper
{
    public static void AddSqlDataModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.ScanDependencyInjection(Assembly.GetExecutingAssembly(), "Repository");

        services.AddDbContext<StoreContext>(options =>
        {
            var connection = configuration.GetConnectionString("ProdutosContext") ??
                throw new InvalidOperationException("Connection string 'StoreContext' not found.");

            options.UseSqlServer(connection, b => b.MigrationsAssembly("Store.Infra.Data.Sql"));
        });
    }
}
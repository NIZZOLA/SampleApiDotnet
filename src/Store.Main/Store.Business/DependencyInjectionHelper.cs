using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Domain;
using Store.Infra.Data;
using System.Reflection;

namespace Store.Business;
public static class DependencyInjectionHelper
{
    public static void AddBusiness(this IServiceCollection services, IConfiguration configuration)
    {
        services.ScanDependencyInjection(Assembly.GetExecutingAssembly(), "Service");
        services.AddDataModule(configuration);
    }
}

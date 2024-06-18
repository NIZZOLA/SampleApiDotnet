using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Store.Business;
using Store.Domain;

namespace Store.Application;
public static class DependencyInjectionHelper
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.ScanDependencyInjection(Assembly.GetExecutingAssembly(), "AppService");
        //services.AddBusiness(configuration);
    }
}

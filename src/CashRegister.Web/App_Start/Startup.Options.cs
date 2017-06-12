using CashRegister.Web.Models.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashRegister.Web.App_Start
{
    public static class Startup_Options
    {
        public static void AddCustomOptions(this IServiceCollection services, IConfigurationRoot conf)
        {
            services.Configure<AuthenticationSettings>(conf.GetSection("TokenAuthentication"));
            services.Configure<OperationSettings>(conf.GetSection("Operations"));
        }
    }
}

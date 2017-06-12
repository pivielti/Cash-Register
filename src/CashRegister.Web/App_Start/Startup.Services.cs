using CashRegister.Web.Services;
using CashRegister.Web.Services.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace CashRegister.Web.App_Start
{
    public static class Startup
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOperationService, OperationService>();
            services.AddTransient<ICashRegistrationService, CashRegistrationService>();
        }
    }
}

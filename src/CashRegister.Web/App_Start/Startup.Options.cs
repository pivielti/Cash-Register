using CashRegister.Web.Models.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashRegister.Web.App_Start
{
    public static class Startup_Options
    {
        public static void AddCustomOptions(this IServiceCollection services, IConfigurationRoot conf)
        {
            services.Configure<AuthenticationOptions>(conf.GetSection("TokenAuthentication"));
        }
    }
}

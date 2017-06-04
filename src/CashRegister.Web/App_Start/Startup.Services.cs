using CashRegister.Web.Services;
using CashRegister.Web.Services.Impl;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashRegister.Web.App_Start
{
    public static class Startup
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthenticationService, AuthenticationService>();
        }
    }
}

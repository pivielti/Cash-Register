using CashRegister.Web.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CashRegister.Web.App_Start
{
    public static class Startup_DbContext
    {
        public static void MigrateDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
                serviceScope.ServiceProvider.GetService<ApplicationDbContext>().CreateDefaultAccount();
            }
        }
    }
}

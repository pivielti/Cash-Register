using CashRegister.Web.Models.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CashRegister.Web.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Operation> Operations { get; set; }

        public DbSet<OperationDetail> OperationDetails { get; set; }

        public DbSet<CashRegistration> CashRegistrations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public async void CreateAdminAccount(UserManager<ApplicationUser> userMgr, RoleManager<IdentityRole> roleMgr)
        {
            if (!Users.Any(u => u.UserName == "administrator"))
            {
                // create admin role
                var adminRole = await roleMgr.FindByNameAsync("admin");
                if (adminRole == null)
                {
                    adminRole = new IdentityRole("admin");
                    await roleMgr.CreateAsync(adminRole);
                }

                // create admin user
                var adminUser = new ApplicationUser() {
                    UserName = "administrator"
                };

                // ... with default password
                var createResult = await userMgr.CreateAsync(adminUser, "password");

                // ... activated
                var lockoutResult = await userMgr.SetLockoutEnabledAsync(adminUser, false);

                // ... and with full privileges
                var addRoleResult = await userMgr.AddToRoleAsync(adminUser, "admin");
            }
        }
    }
}

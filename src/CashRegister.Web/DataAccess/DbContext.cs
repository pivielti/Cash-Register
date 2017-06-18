using CashRegister.Web.Models.DbContext;
using CashRegister.Web.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CashRegister.Web.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Operation> Operations { get; set; }

        public DbSet<OperationDetail> OperationDetails { get; set; }

        public DbSet<CashRegistration> CashRegistrations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>()
            .HasKey(t => new { t.UserId, t.RoleId });

            builder.Entity<UserRole>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(pt => pt.UserId);

            builder.Entity<UserRole>()
                .HasOne(pt => pt.Role)
                .WithMany(t => t.UserRoles)
                .HasForeignKey(pt => pt.RoleId);
        }

        public async void CreateDefaultAccount()
        {
            if (!Users.Any(u => u.Login == "administrator"))
            {
                // create admin role
                var adminRole = new Role() {
                    Name = "admin"
                };
                Roles.Add(adminRole);

                // create admin user
                var salt = string.Empty;
                var hash = AuthHelper.HashPassword("password", out salt);
                var adminUser = new User() {
                    Login = "administrator",
                    PasswordHash = hash,
                    HashSalt = salt
                };

                // link user and role
                UserRoles.Add(new UserRole {
                    Role = adminRole,
                    User = adminUser
                });

                await SaveChangesAsync();
            }
        }
    }
}

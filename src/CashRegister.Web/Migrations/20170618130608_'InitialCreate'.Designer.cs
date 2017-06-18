using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CashRegister.Web.DataAccess;
using CashRegister.Web.Models.DbContext;

namespace CashRegister.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170618130608_'InitialCreate'")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("CashRegister.Web.Models.DbContext.CashRegistration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Cash");

                    b.Property<DateTime>("Moment");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("CashRegistrations");
                });

            modelBuilder.Entity("CashRegister.Web.Models.DbContext.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Color")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CashRegister.Web.Models.DbContext.Operation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Moment");

                    b.Property<bool>("Reduced");

                    b.HasKey("Id");

                    b.ToTable("Operations");
                });

            modelBuilder.Entity("CashRegister.Web.Models.DbContext.OperationDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<int>("OperationId");

                    b.Property<string>("ProductName");

                    b.Property<decimal>("ProductPrice");

                    b.HasKey("Id");

                    b.HasIndex("OperationId");

                    b.ToTable("OperationDetails");
                });

            modelBuilder.Entity("CashRegister.Web.Models.DbContext.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<decimal>("CostPrice");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CashRegister.Web.Models.DbContext.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("CashRegister.Web.Models.DbContext.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("HashSalt")
                        .IsRequired();

                    b.Property<string>("Login")
                        .IsRequired();

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CashRegister.Web.Models.DbContext.UserRole", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("CashRegister.Web.Models.DbContext.OperationDetail", b =>
                {
                    b.HasOne("CashRegister.Web.Models.DbContext.Operation", "Operation")
                        .WithMany("Details")
                        .HasForeignKey("OperationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CashRegister.Web.Models.DbContext.Product", b =>
                {
                    b.HasOne("CashRegister.Web.Models.DbContext.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CashRegister.Web.Models.DbContext.UserRole", b =>
                {
                    b.HasOne("CashRegister.Web.Models.DbContext.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CashRegister.Web.Models.DbContext.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

using CreditoApp.Domain.Entities.Shared;
using CreditoApp.Domain.Entitites.Auth;
using CreditoApp.Domain.Entitites.CreditApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CreditoApp.Infrastructure.Database
{


    public class DatabaseContext : DbContext
    {

        private readonly string _connectionString;

        public DatabaseContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'Default' not found.");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Users
            modelBuilder.Entity<User>()
                .ToTable("Users")
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId);


            // Roles
            modelBuilder.Entity<Role>()
                .ToTable("Roles")
                .HasKey(r => r.Id);

            // UserRoles
            modelBuilder.Entity<UserRoles>()
                .ToTable("UserRoles");

            modelBuilder.Entity<UserRoles>()
                .HasKey(ur => ur.Id); // Asegúrate de tener esta clave primaria

            modelBuilder.Entity<UserRoles>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRoles>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // CreditRequests
            modelBuilder.Entity<CreditRequest>()
                .ToTable("CreditRequests")
                .HasKey(cr => cr.Id);

            modelBuilder.Entity<Audit>()
                .ToTable("Audits")
                .HasKey(a => a.Id);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<CreditRequest> CreditRequests { get; set; }
        public DbSet<Audit> Audits { get; set; } = null!;
    }
}
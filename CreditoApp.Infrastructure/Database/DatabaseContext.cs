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
            _connectionString = configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string 'Default' not found.");
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
            modelBuilder.Entity<User>().ToTable("Users").HasKey(u => u.Id);

            modelBuilder.Entity<Role>().ToTable("UserRoles").HasKey(r => r.Id);

            modelBuilder.Entity<UserRoles>()
                .ToTable("UserRoles")
                .HasOne(ur => ur.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<CreditRequest>()
                .ToTable("CreditRequests")
                .HasKey(cr => cr.Id);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> UserRoles { get; set; }
        public DbSet<UserRoles> CreditRequestHistories { get; set; }
        public DbSet<CreditRequest> CreditRequests { get; set; }
    }
}
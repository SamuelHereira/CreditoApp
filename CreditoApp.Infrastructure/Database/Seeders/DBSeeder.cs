
using CreditoApp.Domain.Entitites.Auth;

namespace CreditoApp.Infrastructure.Database.Seeders
{
    public static class DbSeeder
    {
        public static void Seed(DatabaseContext context)
        {
            if (!context.Roles.Any())
            {
                var roles = new List<Role>
                {
                    new Role { Name = "Applicant" },
                    new Role { Name = "Analyst" }
                };
                context.Roles.AddRange(roles);
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var analyst = new User
                {
                    Name = "Analyst",
                    Lastname = "User",
                    Email = "analyst@creditoapp.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Analyst123"),
                    UserRoles = new List<UserRoles>
                    {
                        new UserRoles { Role = context.Roles.First(r => r.Name == "Analyst") }
                    }
                };

                context.Users.Add(analyst);
            }

            context.SaveChanges();
        }
    }
}

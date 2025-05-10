using CreditoApp.Domain.Entitites.Auth;
using CreditoApp.Infrastructure.Database;
using CreditoApp.Infrastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CreditoApp.Infrastructure.Repositories
{

    public class AuthRepository : IAuthRepository
    {
        private readonly DatabaseContext _context;

        public AuthRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }
    }
}
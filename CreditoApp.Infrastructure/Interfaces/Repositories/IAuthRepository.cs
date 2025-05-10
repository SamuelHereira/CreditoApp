using CreditoApp.Domain.Entitites.Auth;

namespace CreditoApp.Infrastructure.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        Task<User> CreateUser(User user);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(int userId);
    }
}
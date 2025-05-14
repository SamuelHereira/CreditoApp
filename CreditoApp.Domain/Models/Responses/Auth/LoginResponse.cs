
using CreditoApp.Domain.Entitites.Auth;

namespace CreditoApp.Domain.Models.Responses.Auth
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public List<string> Roles { get; set; } = new();

        public User? User { get; set; } = null!;
    }
}

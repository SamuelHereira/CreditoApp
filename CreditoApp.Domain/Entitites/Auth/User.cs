using CreditoApp.Domain.Entitites.Shared;

namespace CreditoApp.Domain.Entitites.Auth
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
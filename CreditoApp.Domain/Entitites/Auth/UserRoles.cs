using CreditoApp.Domain.Entitites.Shared;

namespace CreditoApp.Domain.Entitites.Auth
{
    public class UserRoles : BaseEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public User User { get; set; } = new User();
        public Role Role { get; set; } = new Role();
    }
}
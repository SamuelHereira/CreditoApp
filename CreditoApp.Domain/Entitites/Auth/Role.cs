using CreditoApp.Domain.Entitites.Shared;

namespace CreditoApp.Domain.Entitites.Auth
{
    public class Role : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<UserRoles> UserRoles { get; set; } = new List<UserRoles>();
    }
}
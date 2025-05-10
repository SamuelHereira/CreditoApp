using CreditoApp.Domain.Entitites.Auth;
using CreditoApp.Domain.Entitites.Shared;
using CreditoApp.Domain.Enums;

namespace CreditoApp.Domain.Entitites.CreditApp
{
    public class CreditRequest : BaseEntity
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public int TermMonths { get; set; }
        public decimal MonthlyIncome { get; set; }
        public int JobSeniorityYears { get; set; }
        public DateTime RequestDate { get; set; }
        public CreditStatus Status { get; set; } = CreditStatus.Pending;
        public User User { get; set; } = null!;
    }
}
using CreditoApp.Domain.Enums;

namespace CreditoApp.Domain.Models.Requests.CreditApp
{
    public class UpdateCreditRequestStatus
    {
        public CreditStatus Status { get; set; }
    }
}
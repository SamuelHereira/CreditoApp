namespace CreditoApp.Domain.Models.Responses.CreditApp
{
    public class CreditRequestResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public int TermMonths { get; set; }
        public decimal MonthlyIncome { get; set; }
        public int JobSeniorityYears { get; set; }

        public DateTime RequestDate { get; set; }
        public string Status { get; set; } = null!;

    }
}
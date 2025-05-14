namespace CreditoApp.Domain.Entities.Shared;

public class Audit
{
    public int Id { get; set; }
    public string Action { get; set; } = null!;
    public string Entity { get; set; } = null!;
    public string? Details { get; set; }
    public int? UserId { get; set; }
    public DateTime PerformedAt { get; set; } = DateTime.UtcNow;
}

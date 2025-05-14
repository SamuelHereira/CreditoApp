using CreditoApp.Domain.Entities.Shared;
using CreditoApp.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
public class AuditLogger
{
    private readonly DatabaseContext _context;
    private readonly IHttpContextAccessor _httpContext;

    public AuditLogger(DatabaseContext context, IHttpContextAccessor httpContext)
    {
        _context = context;
        _httpContext = httpContext;
    }

    public async Task Log(string action, string entity, string details)
    {
        var user = _httpContext.HttpContext?.User?.Identity?.Name ?? "Anonymous";

        _context.Audits.Add(new Audit
        {
            Action = action,
            Entity = entity,
            Details = details,
            UserId = _httpContext.HttpContext?.User?.FindFirst("UserId")?.Value != null ? int.Parse(_httpContext.HttpContext.User.FindFirst("UserId").Value) : null,
            PerformedAt = DateTime.UtcNow
        });

        await _context.SaveChangesAsync();
    }
}

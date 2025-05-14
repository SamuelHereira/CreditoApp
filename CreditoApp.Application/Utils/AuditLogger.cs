using CreditoApp.Domain.Entities.Shared;
using CreditoApp.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
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
       var userId = int.Parse(_httpContext.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

        _context.Audits.Add(new Audit
        {
            Action = action,
            Entity = entity,
            Details = details,
            UserId = userId,
            PerformedAt = DateTime.UtcNow
        });

        await _context.SaveChangesAsync();
    }
}

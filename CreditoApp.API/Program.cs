using System.Text;
using CreditoApp.Application.Interfaces.Services;
using CreditoApp.Application.Services;
using CreditoApp.Application.Utils;
using CreditoApp.Domain.Models.Responses.Shared;
using CreditoApp.Infrastructure.Database;
using CreditoApp.Infrastructure.Database.Seeders;
using CreditoApp.Infrastructure.Interfaces.Repositories;
using CreditoApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using CreditoApp.Domain.Exceptions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddExceptionHandler(options =>
{

    options.ExceptionHandler = async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>().Error;

        int statusCode = 500;
        string message = "Internal Server Error";

        if (exception is AuthException)
        {
            statusCode = 401;
            message = "Unauthorized";
        }
        else if (exception is ClientFaultException)
        {
            statusCode = 400;
            message = "Bad Request";
        }
        else if (exception is ServerFaultException)
        {
            statusCode = 500;
            message = "Internal Server Error";
        }

        context.Response.StatusCode = statusCode;

        var errorResponse = new ErrorResponse(statusCode, message, new Error
        {
            Code = statusCode,
            ErrorMessage = message
        });

        if (exception is CustomException)
        {
            statusCode = ((CustomException)exception).Code;
            message = ((CustomException)exception).Message;
        }

        errorResponse.Error = new Error
        {
            Code = statusCode,
            ErrorMessage = message
        };

        await context.Response.WriteAsJsonAsync(errorResponse);
        await context.Response.CompleteAsync();
    };


});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "CreditoApp API", Version = "v1" });
    c.CustomSchemaIds(type => type.ToString());
});

builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICreditReviewService, CreditReviewService>();
builder.Services.AddScoped<ICreditRequestService, CreditRequestService>();

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ICreditReviewRepository, CreditReviewRepository>();
builder.Services.AddScoped<ICreditRequestRepository, CreditRequestRepository>();

builder.Services.AddScoped<JWTUtils>();
builder.Services.AddScoped<PasswordUtils>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    dbContext.Database.Migrate();
    DbSeeder.Seed(dbContext);
}


app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:5173")
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials();
});

app.Run();
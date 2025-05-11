using CreditoApp.Domain.Models.Requests.Auth;
using CreditoApp.Domain.Models.Responses.Auth;
using CreditoApp.Domain.Models.Responses.Shared;

namespace CreditoApp.Application.Interfaces.Services
{

    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task<RegisterResponse> Register(RegisterRequest request);
    }
}
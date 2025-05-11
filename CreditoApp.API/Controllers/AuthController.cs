using CreditoApp.Application.Interfaces.Services;
using CreditoApp.Domain.Models.Requests.Auth;
using CreditoApp.Domain.Models.Responses.Auth;
using CreditoApp.Domain.Models.Responses.Shared;
using Microsoft.AspNetCore.Mvc;

namespace CreditoApp.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<SuccessResponse<LoginResponse>>> Login(LoginRequest request)
        {
            var response = await _authService.Login(request);
            return Ok(new SuccessResponse<LoginResponse>(200, "Login successfull", response));
        }

        [HttpPost("register")]
        public async Task<ActionResult<SuccessResponse<RegisterResponse>>> Register(RegisterRequest request)
        {
            var response = await _authService.Register(request);
            return Ok(new SuccessResponse<RegisterResponse>(200, "User registered successfully", response));
        }
    }
}
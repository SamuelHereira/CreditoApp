
using CreditoApp.Application.Interfaces.Services;
using CreditoApp.Application.Utils;
using CreditoApp.Domain.Entitites.Auth;
using CreditoApp.Domain.Models.Requests.Auth;
using CreditoApp.Domain.Models.Responses.Auth;
using CreditoApp.Domain.Models.Responses.Shared;
using CreditoApp.Infrastructure.Interfaces.Repositories;
using CreditoApp.Domain.Exceptions;
namespace CreditoApp.Application.Services
{
    public class AuthService : IAuthService
    {

        private readonly IAuthRepository _authRepository;
        private readonly PasswordUtils _passwordUtils;
        private readonly JWTUtils _jwtUtils;

        public AuthService(IAuthRepository authRepository, PasswordUtils passwordUtils, JWTUtils jwtUtils)
        {
            _passwordUtils = passwordUtils;
            _jwtUtils = jwtUtils;
            _authRepository = authRepository;
        }
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user = await _authRepository.GetUserByEmail(request.Email);
            if (user == null)
            {
                throw new AuthException("Usuario o contraseña incorrectos.");
            }

            // Validate password
            if (_passwordUtils.VerifyPassword(request.Password, user.Password) == false)
            {
                throw new AuthException("Usuario o contraseña incorrectos.");
            }

            // Generate JWT token
            var token = _jwtUtils.GenerateToken(user);

            return new LoginResponse
            {
                Id = user.Id,
                Token = token,
                Email = user.Email,
                Name = user.Name,
                Lastname = user.Lastname,
                Roles = user.UserRoles.Select(ur => ur.Role.Name).ToList()
            };

        }

        public async Task<RegisterResponse> Register(RegisterRequest request)
        {
            var user = await _authRepository.GetUserByEmail(request.Email);
            if (user != null)
            {
                throw new AuthException("El usuario ya existe.");
            }

            // Hash password
            var hashedPassword = _passwordUtils.HashPassword(request.Password);

            // Create user
            var newUser = new User
            {
                Name = request.Name,
                Lastname = request.Lastname,
                Email = request.Email,
                Password = hashedPassword,
            };

            var createdUser = await _authRepository.CreateUser(newUser);

            // Generate JWT token
            var token = _jwtUtils.GenerateToken(createdUser);

            return new RegisterResponse
            {

            };

        }
    }
}

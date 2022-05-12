using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AdvertisingAgency.BLL.Configs;
using AdvertisingAgency.BLL.Exceptions;
using AdvertisingAgency.BLL.Helpers;
using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Models.Requests;
using AdvertisingAgency.BLL.Models.Responses;
using AdvertisingAgency.DAL.Constraints;
using AdvertisingAgency.DAL.Entities;
using AdvertisingAgency.DAL.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AdvertisingAgency.BLL.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtConfig _jwtConfig;

    public AuthService(IUnitOfWork unitOfWork, IOptions<JwtConfig> options)
    {
        _unitOfWork = unitOfWork;
        _jwtConfig = options.Value;
    }
    
    public async Task<AuthResponse> LoginAsync(LoginRequest model)
    {
        var user = await _unitOfWork.UserRepository.GetSingleByExpressionAsync(t => t.Email == model.Email);
        if (user is null) throw new HttpException(HttpStatusCode.Unauthorized, "Wrong data");
        var isVerified = BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash);
        if (!isVerified) throw new HttpException(HttpStatusCode.Unauthorized, "Wrong credentials");
        var jwt = JwtHelper.GenerateJwt(user.Id, user.Email, user.Role, _jwtConfig);
        return new AuthResponse(jwt);
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest model)
    {
        var user = await _unitOfWork.UserRepository.GetSingleByExpressionAsync(t => t.Email == model.Email);
        if(user is not null) throw new HttpException(HttpStatusCode.Conflict, "That email is already registered");
        var newUser = new User
        {
            Id = Guid.NewGuid().ToString(),
            Email = model.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
            FullName = model.Fullname,
            Role = RoleConstraints.UserRole
        };
        var result = await _unitOfWork.UserRepository.InsertAsync(newUser);
        if (!result) throw new HttpException(HttpStatusCode.InternalServerError, "Server error");
        var jwt = JwtHelper.GenerateJwt(newUser.Id, newUser.Email, newUser.Role, _jwtConfig);
        return new AuthResponse(jwt);
    }

    public async Task<ManagerResponse> CreateManagerAccountAsync()
    {
        var generatedPassword = PasswordHelper.GenerateToken(8);
        var generatedEmail = $"manager{Guid.NewGuid().ToString().Substring(0, 5)}@agency.com";
        var manager = new User
        {
            Id = Guid.NewGuid().ToString(),
            Email = generatedEmail,
            FullName = generatedEmail,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(generatedPassword),
            Role = RoleConstraints.ManagerRole
        };
        var result = await _unitOfWork.UserRepository.InsertAsync(manager);
        if (!result) throw new HttpException(HttpStatusCode.InternalServerError, "Server error");
        return new ManagerResponse(generatedEmail, generatedPassword);
    }
}
using AdvertisingAgency.BLL.Models.Requests;
using AdvertisingAgency.BLL.Models.Responses;

namespace AdvertisingAgency.BLL.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(LoginRequest model);
    Task<AuthResponse> RegisterAsync(RegisterRequest model);
    Task<ManagerResponse> CreateManagerAccountAsync();
}
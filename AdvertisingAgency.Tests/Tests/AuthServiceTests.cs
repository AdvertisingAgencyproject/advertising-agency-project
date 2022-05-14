using System.Threading.Tasks;
using AdvertisingAgency.BLL.Configs;
using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Models.Requests;
using AdvertisingAgency.BLL.Services;
using AdvertisingAgency.DAL;
using AdvertisingAgency.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Xunit;

namespace AdvertisingAgency.Tests.Tests;

public class AuthServiceTests
{
    private readonly IAuthService _authService;
    private readonly IUnitOfWork _unitOfWork;
    
    public AuthServiceTests()
    {
        var dbOptions = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase("FavorServiceTestsDb")
            .Options;
        var dbContext = new DataContext(dbOptions);
        _unitOfWork = new UnitOfWork(dbContext);
        var options = Options.Create(new JwtConfig()
        {
            Audience = "test",
            Issuer = "Test",
            Secret = "asdv234234^&%&^%&^hjsdfb2%%%"
        });
        _authService = new AuthService(_unitOfWork, options);
    }

    [Fact]
    public async Task RegisterTest()
    {
        //Arrange
        var request = new RegisterRequest
        {
            Email = "test@gmail.com",
            Fullname = "test",
            Password = "Qwerty1-"
        };
        //Act
        var result = await _authService.RegisterAsync(request);
        //Assert
        var user = await _unitOfWork.UserRepository.GetSingleByExpressionAsync(t => t.Email == request.Email);
        Assert.NotNull(result);
        Assert.Equal(request.Email, user.Email);
    }

    [Fact]
    public async Task LoginAsync()
    {
        //Arrange
        var registerRequest = new RegisterRequest
        {
            Email = "user@gmail.com",
            Fullname = "test",
            Password = "Qwerty1-"
        };
        var loginRequest = new LoginRequest
        {
            Email = "user@gmail.com",
            Password = "Qwerty1-"
        };
        await _authService.RegisterAsync(registerRequest);
        //Act
        var result = await _authService.LoginAsync(loginRequest);
        //Assert
        Assert.NotNull(result);
    }
}
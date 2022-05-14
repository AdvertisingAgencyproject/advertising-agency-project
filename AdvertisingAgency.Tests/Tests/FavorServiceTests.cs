using System.Threading.Tasks;
using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Models.Requests;
using AdvertisingAgency.BLL.Services;
using AdvertisingAgency.DAL;
using AdvertisingAgency.DAL.Entities;
using AdvertisingAgency.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AdvertisingAgency.Tests.Tests;

public class FavorServiceTests
{
    private readonly IFavorService _favorService;
    private readonly IUnitOfWork _unitOfWork;
    
    public FavorServiceTests()
    {
        var dbOptions = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase("FavorServiceTestsDb")
            .Options;
        var dbContext = new DataContext(dbOptions);
        _unitOfWork = new UnitOfWork(dbContext);
        _favorService = new FavorService(_unitOfWork);
    }
    
    [Fact]
    public async Task GetFavorTest()
    {
        //Arrange
        await _unitOfWork.FavorRepository.InsertAsync(new Favor
        {
            Id = "777",
            Title = "Test",
            Type = "Test",
            ImagePath = "Test",
            Price = 100
        });
        //Act
        var result = await _favorService.GetFavorByIdAsync("777");
        //Assert
        Assert.NotNull(result);
        Assert.Equal("777", result.Id);
    }
    
    [Fact]
    public async Task GetFavorsTest()
    {
        //Arrange
        await _unitOfWork.FavorRepository.InsertAsync(new Favor
        {
            Id = "1",
            Title = "Test",
            Type = "Test",
            ImagePath = "Test",
            Price = 100
        });
        await _unitOfWork.FavorRepository.InsertAsync(new Favor
        {
            Id = "2",
            Title = "Test",
            Type = "Test",
            ImagePath = "Test",
            Price = 100
        });
        //Act
        var result = await _favorService.GetFavorsAsync();
        //Assert
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task UpdateFavorTest()
    {
        //Arrange
        await _unitOfWork.FavorRepository.InsertAsync(new Favor
        {
            Id = "3",
            Title = "Test",
            Type = "Test",
            ImagePath = "Test",
            Price = 25
        });
        var request = new FavorUpdateRequest
        {
            Id = "3",
            Title = "Updated",
            Type = "Test",
            ImagePath = "Test",
            Price = 25
        };
        //Act
        await _favorService.UpdateFavorAsync(request);
        //Assert
        var result = await _unitOfWork.FavorRepository.GetByIdAsync("3");
        Assert.NotNull(result);
        Assert.Equal(request.Title, result.Title);
    }
    
    [Fact]
    public async Task DeleteFavorTest()
    {
        //Arrange
        await _unitOfWork.FavorRepository.InsertAsync(new Favor
        {
            Id = "4",
            Title = "Test",
            Type = "Test",
            ImagePath = "Test",
            Price = 25
        });
        //Act
        await _favorService.DeleteFavorAsync("4");
        //Assert
        var result = await _unitOfWork.FavorRepository.GetByIdAsync("4");
        Assert.Null(result);
    }
}
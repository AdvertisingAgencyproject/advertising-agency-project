using System;
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

public class ProductServiceTests
{
    private readonly IProductService _productService;
    private readonly IUnitOfWork _unitOfWork;
    
    public ProductServiceTests()
    {
        var dbOptions = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase("ProductServiceTestsDb")
            .Options;
        var dbContext = new DataContext(dbOptions);
        _unitOfWork = new UnitOfWork(dbContext);
        _productService = new ProductService(_unitOfWork);
    }

    [Fact]
    public async Task GetProductTest()
    {
        //Arrange
        await _unitOfWork.ProductRepository.InsertAsync(new Product
        {
            Id = "1",
            Text = "Test",
            Type = "Test",
            ImagePath = "Test",
            Price = 100
        });
        //Act
        var result = await _productService.GetProductByIdAsync("1");
        //Assert
        Assert.NotNull(result);
        Assert.Equal("1", result.Id);
    }

    [Fact]
    public async Task GetProductsTest()
    {
        //Arrange
        await _unitOfWork.ProductRepository.InsertAsync(new Product
        {
            Id = "11",
            Text = "Test",
            Type = "Test",
            ImagePath = "Test",
            Price = 100
        });
        await _unitOfWork.ProductRepository.InsertAsync(new Product
        {
            Id = "22",
            Text = "Test",
            Type = "Test",
            ImagePath = "Test",
            Price = 150
        });
        //Act
        var result = await _productService.GetProductsAsync();
        //Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateProductTest()
    {
        //Arrange
        await _unitOfWork.ProductRepository.InsertAsync(new Product
        {
            Id = "3",
            Text = "Test",
            Type = "Test",
            ImagePath = "Test",
            Price = 25
        });
        var request = new ProductUpdateRequest
        {
            Id = "3",
            Text = "Updated",
            Type = "Test",
            ImagePath = "Test",
            Price = 25
        };
        //Act
        await _productService.UpdateProductAsync(request);
        //Assert
        var result = await _unitOfWork.ProductRepository.GetByIdAsync("3");
        Assert.NotNull(result);
        Assert.Equal(request.Text, result.Text);
    }

    [Fact]
    public async Task DeleteProductTest()
    {
        //Arrange
        await _unitOfWork.ProductRepository.InsertAsync(new Product
        {
            Id = "4",
            Text = "Test",
            Type = "Test",
            ImagePath = "Test",
            Price = 25
        });
        //Act
        await _productService.DeleteProductAsync("4");
        //Assert
        var result = await _unitOfWork.ProductRepository.GetByIdAsync("4");
        Assert.Null(result);
    }
}
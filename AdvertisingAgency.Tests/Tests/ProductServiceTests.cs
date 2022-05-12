using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Services;
using AdvertisingAgency.DAL;
using AdvertisingAgency.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Tests.Tests;

public class ProductServiceTests
{
    private readonly IProductService _productService;
    
    private ProductServiceTests()
    {
        var dbOptions = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase("ProductServiceTestsDb")
            .Options;
        var dbContext = new DataContext(dbOptions);
        var unitOfWork = new UnitOfWork(dbContext);
        _productService = new ProductService(unitOfWork);
    }
}
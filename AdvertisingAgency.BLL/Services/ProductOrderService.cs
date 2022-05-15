using System.Net;
using AdvertisingAgency.BLL.Exceptions;
using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Mappers;
using AdvertisingAgency.BLL.Models.Requests;
using AdvertisingAgency.BLL.Models.Responses;
using AdvertisingAgency.DAL.Interfaces;

namespace AdvertisingAgency.BLL.Services;

public class ProductOrderService : IProductOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductOrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateProductOrderAsync(ProductOrderRequest model)
    {
        var entity = model.MapToEntity();
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(model.ProductId);
        entity.Id = Guid.NewGuid().ToString();
        entity.Created = DateTime.UtcNow;
        entity.TotalPrice = product.Price * entity.Count;
        var result = await _unitOfWork.ProductOrderRepository.InsertAsync(entity);
        if (!result) throw new HttpException(HttpStatusCode.InternalServerError, "Server error");
    }

    public async Task<List<ProductOrderResponse>> GetUserProductOrdersAsync(string userId)
    {
        var entities = await _unitOfWork.ProductOrderRepository.GetManyByExpressionAsync(t => t.UserId == userId);
        return entities.MapToResponseList();
    }

    public async Task<List<ProductOrderResponse>> GetAllProductOrdersAsync()
    {
        var entities = await _unitOfWork.ProductOrderRepository.GetAllAsync();
        return entities.MapToResponseList();
    }
}
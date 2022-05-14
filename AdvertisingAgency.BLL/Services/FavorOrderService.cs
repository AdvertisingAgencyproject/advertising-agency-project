using System.Net;
using AdvertisingAgency.BLL.Exceptions;
using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Mappers;
using AdvertisingAgency.BLL.Models.Requests;
using AdvertisingAgency.BLL.Models.Responses;
using AdvertisingAgency.DAL.Interfaces;

namespace AdvertisingAgency.BLL.Services;

public class FavorOrderService : IFavorOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public FavorOrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateFavorOrderAsync(FavorOrderRequest model)
    {
        var entity = model.MapToEntity();
        var favor = await _unitOfWork.FavorRepository.GetByIdAsync(model.FavorId);
        entity.Id = Guid.NewGuid().ToString();
        entity.Created = DateTime.UtcNow;
        if (favor.Discount != null)
        {
            entity.TotalPrice = favor.Price * favor.Discount.Percents / 100;
        }
        else
        {
            entity.TotalPrice = favor.Price;
        }
        
        var result = await _unitOfWork.FavorOrderRepository.InsertAsync(entity);
        if (!result) throw new HttpException(HttpStatusCode.InternalServerError, "Server error");
    }

    public async Task<List<FavorOrderResponse>> GetUserFavorOrdersAsync(string userId)
    {
        var entities = await _unitOfWork.FavorOrderRepository.GetManyByExpressionAsync(t => t.UserId == userId);
        return entities.MapToResponseList();
    }
}
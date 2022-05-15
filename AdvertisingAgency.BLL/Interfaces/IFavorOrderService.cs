using AdvertisingAgency.BLL.Models.Requests;
using AdvertisingAgency.BLL.Models.Responses;

namespace AdvertisingAgency.BLL.Interfaces;

public interface IFavorOrderService
{
    Task CreateFavorOrderAsync(FavorOrderRequest model);
    Task<List<FavorOrderResponse>> GetUserFavorOrdersAsync(string userId);
    Task<List<FavorOrderResponse>> GetAllFavorOrdersAsync();
}
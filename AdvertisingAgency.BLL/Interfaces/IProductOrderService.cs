using AdvertisingAgency.BLL.Models.Requests;
using AdvertisingAgency.BLL.Models.Responses;
using AdvertisingAgency.DAL.Entities;

namespace AdvertisingAgency.BLL.Interfaces;

public interface IProductOrderService
{
    Task CreateProductOrderAsync(ProductOrderRequest model);
    Task<List<ProductOrderResponse>> GetUserProductOrdersAsync(string userId);
}
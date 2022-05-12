using AdvertisingAgency.BLL.Models.Requests;
using AdvertisingAgency.BLL.Models.Responses;

namespace AdvertisingAgency.BLL.Interfaces;

public interface IProductService
{
    Task CreateProductAsync(ProductRequest model);
    Task<ProductResponse> GetProductByIdAsync(string id);
    Task<List<ProductResponse>> GetProductsAsync();
    Task UpdateProductAsync(ProductUpdateRequest model);
    Task DeleteProductAsync(string id);
}
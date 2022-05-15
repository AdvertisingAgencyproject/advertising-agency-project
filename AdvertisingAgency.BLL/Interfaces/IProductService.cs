using AdvertisingAgency.BLL.Models.Requests;
using AdvertisingAgency.BLL.Models.Responses;

namespace AdvertisingAgency.BLL.Interfaces;

public interface IProductService
{
    Task CreateProductAsync(ProductRequest model);
    Task<ProductResponse> GetProductByIdAsync(string id);
    Task<List<ProductResponse>> GetProductsAsync(string searchQuery, string typeFilter, int minPrice, int maxPrice);
    Task UpdateProductAsync(ProductUpdateRequest model);
    Task DeleteProductAsync(string id);
    Task<ProductFilterResponse> GetProductFiltersAsync();
    Task<List<ProductResponse>> GetProductsAsync();
}
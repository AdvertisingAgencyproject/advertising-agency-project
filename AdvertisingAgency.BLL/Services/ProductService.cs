using System.Net;
using AdvertisingAgency.BLL.Exceptions;
using AdvertisingAgency.BLL.Helpers;
using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Mappers;
using AdvertisingAgency.BLL.Models.Requests;
using AdvertisingAgency.BLL.Models.Responses;
using AdvertisingAgency.DAL.Interfaces;

namespace AdvertisingAgency.BLL.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateProductAsync(ProductRequest model)
    {
        var newProduct = model.MapToEntity();
        newProduct.Id = Guid.NewGuid().ToString();
        newProduct.ImagePath = await ImageHelper.SaveImageAsync(model.Base64);
        var result = await _unitOfWork.ProductRepository.InsertAsync(newProduct);
        if (!result) throw new HttpException(HttpStatusCode.InternalServerError, "Server error");
    }

    public async Task<ProductResponse> GetProductByIdAsync(string id)
    {
        var entity = await _unitOfWork.ProductRepository.GetByIdAsync(id);
        return entity.MapToResponse();
    }

    public async Task<List<ProductResponse>> GetProductsAsync(ProductFilterRequest model)
    {
        var entities = await _unitOfWork.ProductRepository.GetAllAsync();
        if (model.SearchQuery != "%default%")
        {
            entities = entities.Where(t => t.Text.ToUpper().Contains(model.SearchQuery.ToUpper()))
                               .ToList();
        }
        
        entities = entities.Where(t => t.Price >= model.MinPrice && t.Price <= model.MaxPrice)
                           .ToList();
        
        return entities.MapToResponseList();
    }

    public async Task UpdateProductAsync(ProductUpdateRequest model)
    {
        var entity = model.MapToEntity();
        var result = await _unitOfWork.ProductRepository.UpdateAsync(entity);
        if (!result) throw new HttpException(HttpStatusCode.InternalServerError, "Server error");
    }

    public async Task DeleteProductAsync(string id)
    {
        var entity = await _unitOfWork.ProductRepository.GetByIdAsync(id);
        var result = await _unitOfWork.ProductRepository.DeleteAsync(entity);
        if (!result) throw new HttpException(HttpStatusCode.InternalServerError, "Server error");
    }

    public async Task<ProductFilterResponse> GetProductFiltersAsync()
    {
        var products = await _unitOfWork.ProductRepository.GetAllAsync();
        var types = String.Join('%', products.Select(t => t.Type));
            
        return new ProductFilterResponse
        {
            MinPrice = products.Select(t => t.Price).Min(),
            MaxPrice = products.Select(t => t.Price).Max()
        };
    }

    public async Task<List<ProductResponse>> GetProductsAsync()
    {
        var products = await _unitOfWork.ProductRepository.GetAllAsync();
        return products.MapToResponseList();
    }
}
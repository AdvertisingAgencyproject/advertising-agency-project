using AdvertisingAgency.BLL.Models.Requests;
using AdvertisingAgency.BLL.Models.Responses;
using AdvertisingAgency.DAL.Entities;

namespace AdvertisingAgency.BLL.Mappers;

public static class ProductMappers
{
    public static Product MapToEntity(this ProductRequest model)
    {
        return new Product
        {
            Text = model.Text,
            Type = model.Type,
            Price = model.Price
        };
    }

    public static ProductResponse MapToResponse(this Product entity)
    {
        return new ProductResponse
        {
            Id = entity.Id,
            ImagePath = entity.ImagePath,
            Text = entity.Text,
            Type = entity.Type,
            Price = entity.Price
        };
    }

    public static List<ProductResponse> MapToResponseList(this List<Product> entities)
    {
        return entities.Select(t => new ProductResponse
        {
            Id = t.Id,
            ImagePath = t.ImagePath,
            Text = t.Text,
            Type = t.Type,
            Price = t.Price
        }).ToList();
    }

    public static Product MapToEntity(this ProductUpdateRequest model)
    {
        return new Product
        {
            Id = model.Id,
            ImagePath = model.ImagePath,
            Price = model.Price,
            Text = model.Text,
            Type = model.Type
        };
    }
}
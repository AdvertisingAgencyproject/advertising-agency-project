using AdvertisingAgency.BLL.Models.Requests;
using AdvertisingAgency.BLL.Models.Responses;
using AdvertisingAgency.DAL.Entities;

namespace AdvertisingAgency.BLL.Mappers;

public static class ProductOrderMappers
{
    public static ProductOrder MapToEntity(this ProductOrderRequest model)
    {
        return new ProductOrder
        {
            UserId = model.UserId,
            ProductId = model.ProductId,
            Count = model.Count,
            Text = model.Text
        };
    }

    public static ProductOrderResponse MapToResponse(this ProductOrder entity)
    {
        return new ProductOrderResponse
        {
            Id = entity.Id,
            Created = entity.Created,
            ProductId = entity.ProductId,
            UserId = entity.UserId,
            Text = entity.Text,
            Count = entity.Count,
            TotalPrice = entity.TotalPrice
        };
    }

    public static List<ProductOrderResponse> MapToResponseList(this List<ProductOrder> entities)
    {
        return entities.Select(t => new ProductOrderResponse
        {
            Id = t.Id,
            Created = t.Created,
            ProductId = t.ProductId,
            UserId = t.UserId,
            Text = t.Text,
            Count = t.Count,
            TotalPrice = t.TotalPrice
        }).ToList();
    }
}
using AdvertisingAgency.BLL.Models.Requests;
using AdvertisingAgency.BLL.Models.Responses;
using AdvertisingAgency.DAL.Entities;

namespace AdvertisingAgency.BLL.Mappers;

public static class FavorOrderMapper
{
    public static FavorOrder MapToEntity(this FavorOrderRequest model)
    {
        return new FavorOrder
        {
            UserId = model.UserId,
            FavorId = model.FavorId
        };
    }

    public static FavorOrderResponse MapToResponse(this FavorOrder entity)
    {
        return new FavorOrderResponse
        {
            Id = entity.Id,
            Created = entity.Created,
            FavorId = entity.FavorId,
            UserId = entity.UserId,
            TotalPrice = entity.TotalPrice
        };
    }

    public static List<FavorOrderResponse> MapToResponseList(this List<FavorOrder> entities)
    {
        return entities.Select(t => new FavorOrderResponse
        {
            Id = t.Id,
            Created = t.Created,
            FavorId = t.FavorId,
            UserId = t.UserId,
            TotalPrice = t.TotalPrice
        }).ToList();
    }
}
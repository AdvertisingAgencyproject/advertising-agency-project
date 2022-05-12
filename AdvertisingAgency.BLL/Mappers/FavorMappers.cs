using AdvertisingAgency.BLL.Models.Requests;
using AdvertisingAgency.BLL.Models.Responses;
using AdvertisingAgency.DAL.Entities;

namespace AdvertisingAgency.BLL.Mappers;

public static class FavorMappers
{
    public static Favor MapToEntity(this FavorRequest model)
    {
        return new Favor
        {
            Title = model.Title,
            Type = model.Type,
            Price = model.Price
        };
    }

    public static FavorResponse MapToResponse(this Favor entity)
    {
        return new FavorResponse
        {
            Id = entity.Id,
            ImagePath = entity.ImagePath,
            Title = entity.Title,
            Type = entity.Type,
            Price = entity.Price
        };
    }

    public static List<FavorResponse> MapToResponseList(this List<Favor> entities)
    {
        return entities.Select(t => new FavorResponse
        {
            Id = t.Id,
            ImagePath = t.ImagePath,
            Title = t.Title,
            Type = t.Type,
            Price = t.Price
        }).ToList();
    }

    public static Favor MapToEntity(this FavorUpdateRequest model)
    {
        return new Favor
        {
            Id = model.Id,
            ImagePath = model.ImagePath,
            Price = model.Price,
            Title = model.Title,
            Type = model.Type
        };
    }
}
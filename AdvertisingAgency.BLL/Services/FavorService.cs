using System.Net;
using AdvertisingAgency.BLL.Exceptions;
using AdvertisingAgency.BLL.Helpers;
using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Mappers;
using AdvertisingAgency.BLL.Models.Requests;
using AdvertisingAgency.BLL.Models.Responses;
using AdvertisingAgency.DAL.Entities;
using AdvertisingAgency.DAL.Interfaces;

namespace AdvertisingAgency.BLL.Services;

public class FavorService : IFavorService
{
    private readonly IUnitOfWork _unitOfWork;

    public FavorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateFavorAsync(FavorRequest model)
    {
        var newFavor = model.MapToEntity();
        newFavor.Id = Guid.NewGuid().ToString();
        newFavor.ImagePath = await ImageHelper.SaveImageAsync(model.Base64);
        var result = await _unitOfWork.FavorRepository.InsertAsync(newFavor);
        if (!result) throw new HttpException(HttpStatusCode.InternalServerError, "Server error");
    }

    public async Task<FavorResponse> GetFavorByIdAsync(string id)
    {
        var entity = await _unitOfWork.FavorRepository.GetByIdAsync(id);
        return entity.MapToResponse();
    }

    public async Task<List<FavorResponse>> GetFavorsAsync()
    {
        var entities = await _unitOfWork.FavorRepository.GetAllAsync();
        return entities.MapToResponseList();
    }

    public async Task UpdateFavorAsync(FavorUpdateRequest model)
    {
        var entity = model.MapToEntity();
        var result = await _unitOfWork.FavorRepository.UpdateAsync(entity);
        if (!result) throw new HttpException(HttpStatusCode.InternalServerError, "Server error");
    }

    public async Task DeleteFavorAsync(string id)
    {
        var entity = await _unitOfWork.FavorRepository.GetByIdAsync(id);
        var result = await _unitOfWork.FavorRepository.DeleteAsync(entity);
        if (!result) throw new HttpException(HttpStatusCode.InternalServerError, "Server error");
    }

    public async Task AddDiscountToFavorAsync(string favorId, int percents)
    {
        var entity = new Discount
        {
            Id = Guid.NewGuid().ToString(),
            FavorId = favorId,
            Percents = percents
        };

        var result = await _unitOfWork.DiscountRepository.InsertAsync(entity);
        if (!result) throw new HttpException(HttpStatusCode.InternalServerError, "Server error");
    }
}
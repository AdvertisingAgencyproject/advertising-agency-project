using AdvertisingAgency.BLL.Models.Requests;
using AdvertisingAgency.BLL.Models.Responses;

namespace AdvertisingAgency.BLL.Interfaces;

public interface IFavorService
{
    Task CreateFavorAsync(FavorRequest model);
    Task<FavorResponse> GetFavorByIdAsync(string id);
    Task<List<FavorResponse>> GetFavorsAsync();
    Task UpdateFavorAsync(FavorUpdateRequest model);
    Task DeleteFavorAsync(string id);
}
using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Models.Requests;
using FastEndpoints;

namespace AdvertisingAgency.Presentation.Endpoints.Favor;

public class UpdateFavorEndpoint : Endpoint<FavorUpdateRequest>
{
    private readonly IFavorService _favorService;

    public UpdateFavorEndpoint(IFavorService favorService)
    {
        _favorService = favorService;
    }

    public override void Configure()
    {
        Verbs(Http.PUT);
        Routes("api/favor");
        Roles("manager", "admin");
    }

    public override async Task HandleAsync(FavorUpdateRequest req, CancellationToken ct) 
        => await _favorService.UpdateFavorAsync(req);
}
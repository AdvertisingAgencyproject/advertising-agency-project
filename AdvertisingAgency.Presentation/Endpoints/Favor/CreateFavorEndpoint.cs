using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Models.Requests;
using FastEndpoints;

namespace AdvertisingAgency.Presentation.Endpoints.Favor;

public class CreateFavorEndpoint : Endpoint<FavorRequest>
{
    private readonly IFavorService _favorService;

    public CreateFavorEndpoint(IFavorService favorService) => _favorService = favorService;

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("api/favor");
        Roles("manager", "admin");
    }

    public override async Task HandleAsync(FavorRequest req, CancellationToken ct) 
        => await _favorService.CreateFavorAsync(req);
}
using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Models.Responses;
using FastEndpoints;

namespace AdvertisingAgency.Presentation.Endpoints.Favor;

public class GetFavorsEndpoint : EndpointWithoutRequest<List<FavorResponse>>
{
    private readonly IFavorService _favorService;

    public GetFavorsEndpoint(IFavorService favorService) => _favorService = favorService;

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("api/favors");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct) 
        => await SendAsync(await _favorService.GetFavorsAsync());
}
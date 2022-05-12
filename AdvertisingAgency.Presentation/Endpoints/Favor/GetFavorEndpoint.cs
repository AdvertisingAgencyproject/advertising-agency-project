using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Models.Requests;
using AdvertisingAgency.BLL.Models.Responses;
using FastEndpoints;

namespace AdvertisingAgency.Presentation.Endpoints.Favor;

public class GetFavorEndpoint : Endpoint<ByIdRequest, FavorResponse>
{
    private readonly IFavorService _favorService;

    public GetFavorEndpoint(IFavorService favorService) => _favorService = favorService;

    public override void Configure()
    {
        Verbs(Http.GET);
        AllowAnonymous();
        Routes("api/favor/{id}");
    }

    public override async Task HandleAsync(ByIdRequest req, CancellationToken ct) 
        => await SendAsync(await _favorService.GetFavorByIdAsync(req.Id));
}
using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Models.Responses;
using FastEndpoints;

namespace AdvertisingAgency.Presentation.Endpoints.FavorOrder;

public class GetAllFavorOrdersEndpoint : EndpointWithoutRequest<List<FavorOrderResponse>>
{
    private readonly IFavorOrderService _favorOrderService;

    public GetAllFavorOrdersEndpoint(IFavorOrderService favorOrderService) 
        => _favorOrderService = favorOrderService;

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("api/favor/order");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct) 
        => await SendAsync(await _favorOrderService.GetAllFavorOrdersAsync());
}
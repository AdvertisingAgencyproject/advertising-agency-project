using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Models.Requests;
using AdvertisingAgency.BLL.Models.Responses;
using FastEndpoints;

namespace AdvertisingAgency.Presentation.Endpoints.FavorOrder;

public class GetUserFavorOrdersEndpoint : Endpoint<ByIdRequest, List<FavorOrderResponse>>
{
    private readonly IFavorOrderService _favorOrderService;

    public GetUserFavorOrdersEndpoint(IFavorOrderService favorOrderService) 
        => _favorOrderService = favorOrderService;
    
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("api/favor/order/{id}");
        Roles("user");
    }

    public override async Task HandleAsync(ByIdRequest req, CancellationToken ct) 
        => await SendAsync(await _favorOrderService.GetUserFavorOrdersAsync(req.Id));
}
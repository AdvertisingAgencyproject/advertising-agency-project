using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Models.Requests;
using FastEndpoints;

namespace AdvertisingAgency.Presentation.Endpoints.FavorOrder;

public class CreateFavorOrderEndpoint : Endpoint<FavorOrderRequest>
{
    private readonly IFavorOrderService _favorOrderService;

    public CreateFavorOrderEndpoint(IFavorOrderService favorOrderService) 
        => _favorOrderService = favorOrderService;

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("api/favor/order");
        Roles("user");
    }

    public override async Task HandleAsync(FavorOrderRequest req, CancellationToken ct) 
        => await _favorOrderService.CreateFavorOrderAsync(req);
}
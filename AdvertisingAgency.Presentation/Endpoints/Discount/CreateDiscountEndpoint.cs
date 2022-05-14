using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Models.Requests;
using FastEndpoints;

namespace AdvertisingAgency.Presentation.Endpoints.Discount;

public class CreateDiscountEndpoint : Endpoint<DiscountCreateRequest>
{
    private readonly IFavorService _favorService;

    public CreateDiscountEndpoint(IFavorService favorService) => _favorService = favorService;

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("api/favor/discount");
        Roles("manager", "admin");
    }

    public override async Task HandleAsync(DiscountCreateRequest req, CancellationToken ct)
        => await _favorService.AddDiscountToFavorAsync(req.FavorId, req.Percents);
}
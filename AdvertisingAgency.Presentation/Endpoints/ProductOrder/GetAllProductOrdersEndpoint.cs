using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Models.Responses;
using FastEndpoints;

namespace AdvertisingAgency.Presentation.Endpoints.ProductOrder;

public class GetAllProductOrdersEndpoint : EndpointWithoutRequest<List<ProductOrderResponse>>
{
    private readonly IProductOrderService _productOrderService;

    public GetAllProductOrdersEndpoint(IProductOrderService productOrderService) 
        => _productOrderService = productOrderService;
    
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("api/product/order");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct) 
        => await SendAsync(await _productOrderService.GetAllProductOrdersAsync());
}
using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Models.Requests;
using AdvertisingAgency.BLL.Models.Responses;
using FastEndpoints;

namespace AdvertisingAgency.Presentation.Endpoints.ProductOrder;

public class GetUserProductOrdersEndpoint : Endpoint<ByIdRequest, List<ProductOrderResponse>>
{
    private readonly IProductOrderService _productOrderService;

    public GetUserProductOrdersEndpoint(IProductOrderService productOrderService) 
        => _productOrderService = productOrderService;
    
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("api/product/order/{id}");
        Roles("user");
    }

    public override async Task HandleAsync(ByIdRequest req, CancellationToken ct) 
        => await SendAsync(await _productOrderService.GetUserProductOrdersAsync(req.Id));
}
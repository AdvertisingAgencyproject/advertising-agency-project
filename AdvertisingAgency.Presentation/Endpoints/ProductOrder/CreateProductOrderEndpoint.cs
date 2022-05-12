using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Models.Requests;
using FastEndpoints;

namespace AdvertisingAgency.Presentation.Endpoints.ProductOrder;

public class CreateProductOrderEndpoint : Endpoint<ProductOrderRequest>
{
    private readonly IProductOrderService _productOrderService;

    public CreateProductOrderEndpoint(IProductOrderService productOrderService) 
        => _productOrderService = productOrderService;

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("api/product/order");
        Roles("user");
    }

    public override async Task HandleAsync(ProductOrderRequest req, CancellationToken ct) 
        => await _productOrderService.CreateProductOrderAsync(req);
}
using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Models.Responses;
using FastEndpoints;

namespace AdvertisingAgency.Presentation.Endpoints.Product;

public class GetProductsEndpoint : EndpointWithoutRequest<List<ProductResponse>>
{
    private readonly IProductService _productService;

    public GetProductsEndpoint(IProductService productService) => _productService = productService;

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("api/products");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct) 
        => await SendAsync(await _productService.GetProductsAsync());
}
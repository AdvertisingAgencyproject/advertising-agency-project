using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Models.Responses;
using FastEndpoints;

namespace AdvertisingAgency.Presentation.Endpoints.Product;

public class GetProductFiltersEndpoint : EndpointWithoutRequest<ProductFilterResponse>
{
    private readonly IProductService _productService;

    public GetProductFiltersEndpoint(IProductService productService) => _productService = productService;
    
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("api/product/filter");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct) 
        => await SendAsync(await _productService.GetProductFiltersAsync());
}
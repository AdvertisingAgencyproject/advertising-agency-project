using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Models.Responses;
using FastEndpoints;

namespace AdvertisingAgency.Presentation.Endpoints.Product;

public class GetProductsWithoutFiltersEndpoint : EndpointWithoutRequest<List<ProductResponse>>
{
    private readonly IProductService _productService;

    public GetProductsWithoutFiltersEndpoint(IProductService productService) => _productService = productService;
    
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("api/products");
        Roles("admin", "manager");
    }

    public override async Task HandleAsync(CancellationToken ct) 
        => await SendAsync(await _productService.GetProductsAsync());
}
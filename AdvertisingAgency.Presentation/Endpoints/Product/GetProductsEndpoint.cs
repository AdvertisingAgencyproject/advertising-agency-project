using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Models.Requests;
using AdvertisingAgency.BLL.Models.Responses;
using FastEndpoints;

namespace AdvertisingAgency.Presentation.Endpoints.Product;

public class GetProductsEndpoint : Endpoint<ProductFilterRequest, List<ProductResponse>>
{
    private readonly IProductService _productService;

    public GetProductsEndpoint(IProductService productService) => _productService = productService;

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("api/products/{searchQuery}/{minPrice}/{maxPrice}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ProductFilterRequest req, CancellationToken ct) 
        => await SendAsync(await _productService.GetProductsAsync(req));
}
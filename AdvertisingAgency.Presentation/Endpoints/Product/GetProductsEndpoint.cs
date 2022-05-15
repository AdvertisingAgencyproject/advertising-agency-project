using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Models.Requests;
using AdvertisingAgency.BLL.Models.Responses;
using FastEndpoints;

namespace AdvertisingAgency.Presentation.Endpoints.Product;

public class GetProductsEndpoint : EndpointWithoutRequest
{
    private readonly IProductService _productService;

    public GetProductsEndpoint(IProductService productService) => _productService = productService;

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("api/products/{searchQuery}/{typeFilter}/{minPrice}/{maxPrice}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var searchQuery = Route<string>("searchQuery");
        var typeFilter = Route<string>("typeFilter");
        var minPrice = Route<int>("minPrice");
        var maxPrice = Route<int>("maxPrice");
        await SendAsync(await _productService.GetProductsAsync(searchQuery, typeFilter, minPrice, maxPrice));
    }
}
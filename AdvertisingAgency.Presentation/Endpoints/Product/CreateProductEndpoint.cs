using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Models.Requests;
using FastEndpoints;

namespace AdvertisingAgency.Presentation.Endpoints.Product;

public class CreateProductEndpoint : Endpoint<ProductRequest>
{
    private readonly IProductService _productService;

    public CreateProductEndpoint(IProductService productService) => _productService = productService;

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("api/product");
        Roles("manager", "admin");
    }

    public override async Task HandleAsync(ProductRequest req, CancellationToken ct) 
        => await _productService.CreateProductAsync(req);
}
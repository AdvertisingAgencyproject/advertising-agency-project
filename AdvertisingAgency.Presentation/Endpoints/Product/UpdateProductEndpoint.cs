using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Models.Requests;
using FastEndpoints;

namespace AdvertisingAgency.Presentation.Endpoints.Product;

public class UpdateProductEndpoint : Endpoint<ProductUpdateRequest>
{
    private readonly IProductService _productService;

    public UpdateProductEndpoint(IProductService productService)
    {
        _productService = productService;
    }

    public override void Configure()
    {
        Verbs(Http.PUT);
        Routes("api/product");
        Roles("manager", "admin");
    }

    public override async Task HandleAsync(ProductUpdateRequest req, CancellationToken ct) 
        => await _productService.UpdateProductAsync(req);
}
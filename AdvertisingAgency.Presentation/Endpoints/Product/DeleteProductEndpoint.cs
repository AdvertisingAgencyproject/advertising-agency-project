using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Models.Requests;
using FastEndpoints;

namespace AdvertisingAgency.Presentation.Endpoints.Product;

public class DeleteProductEndpoint : Endpoint<ByIdRequest>
{
    private readonly IProductService _productService;

    public DeleteProductEndpoint(IProductService productService) => _productService = productService;
    
    public override void Configure()
    {
        Verbs(Http.DELETE);
        Routes("api/product/{id}");
        Roles("manager", "admin");
    }

    public override async Task HandleAsync(ByIdRequest req, CancellationToken ct) 
        => await _productService.DeleteProductAsync(req.Id);
}
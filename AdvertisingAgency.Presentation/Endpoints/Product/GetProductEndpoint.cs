using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Models.Requests;
using AdvertisingAgency.BLL.Models.Responses;
using FastEndpoints;

namespace AdvertisingAgency.Presentation.Endpoints.Product;

public class GetProductEndpoint : Endpoint<ByIdRequest, ProductResponse>
{
    private readonly IProductService _productService;

    public GetProductEndpoint(IProductService productService) => _productService = productService;

    public override void Configure()
    {
        Verbs(Http.GET);
        AllowAnonymous();
        Routes("api/product/{id}");
    }

    public override async Task HandleAsync(ByIdRequest req, CancellationToken ct) 
        => await SendAsync(await _productService.GetProductByIdAsync(req.Id));
}
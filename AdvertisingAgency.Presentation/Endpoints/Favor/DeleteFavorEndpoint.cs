using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Models.Requests;
using FastEndpoints;

namespace AdvertisingAgency.Presentation.Endpoints.Favor;

public class DeleteFavorEndpoint : Endpoint<ByIdRequest>
{
    private readonly IFavorService _favorService;

    public DeleteFavorEndpoint(IFavorService favorService) => _favorService = favorService;
    
    public override void Configure()
    {
        Verbs(Http.DELETE);
        Routes("api/favor/{id}");
        Roles("manager", "admin");
    }

    public override async Task HandleAsync(ByIdRequest req, CancellationToken ct) 
        => await _favorService.DeleteFavorAsync(req.Id);
}
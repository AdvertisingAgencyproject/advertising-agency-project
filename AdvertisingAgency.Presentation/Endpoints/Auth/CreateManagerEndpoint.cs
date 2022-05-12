using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Models.Responses;
using FastEndpoints;

namespace AdvertisingAgency.Presentation.Features.Auth;

public class CreateManagerEndpoint : EndpointWithoutRequest<ManagerResponse>
{
    private readonly IAuthService _authService;

    public CreateManagerEndpoint(IAuthService authService) => _authService = authService;
    
    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("api/admin/manager");
        Roles("admin");
    }

    public override async Task HandleAsync(CancellationToken ct) 
        => await SendAsync(await _authService.CreateManagerAccountAsync());
}
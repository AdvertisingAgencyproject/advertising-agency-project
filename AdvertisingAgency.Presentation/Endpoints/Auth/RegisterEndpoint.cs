using AdvertisingAgency.BLL.Interfaces;
using AdvertisingAgency.BLL.Models.Requests;
using AdvertisingAgency.BLL.Models.Responses;
using AdvertisingAgency.DAL.Constraints;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisingAgency.Presentation.Features.Auth;

public class RegisterEndpoint : Endpoint<RegisterRequest, AuthResponse>
{
    private readonly IAuthService _authService;

    public RegisterEndpoint(IAuthService authService)
    {
        _authService = authService;
    }

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("api/register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
        => await SendOkAsync(await _authService.RegisterAsync(req));
}
using Microsoft.AspNetCore.Mvc;
using TaskMaster.Controllers.Payloads.Requests;
using TaskMaster.Shared;

namespace TaskMaster.Controllers;

public class AuthenticationController : ControllerBase
{
    private readonly string _key;
    private static readonly TimeSpan TokenLifetime = TimeSpan.FromMinutes(5);

    public AuthenticationController(IConfiguration configuration)
    {
        _key = configuration.GetSection(nameof(JwtSettings))
            .Get<JwtSettings>()!
            .Key;
    }

    public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest request)
    {
        return Ok();
    }
}
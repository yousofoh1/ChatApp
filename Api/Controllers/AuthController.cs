using Core.Interfaces;
using Core.Dtos.Auth;
using Core.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers;

public class AuthController(IServicesUOW manager) : BaseController
{

    [HttpPost("login")]
    public async Task<LoginSuccess> Login([FromBody] LoginRequest loginRequest)
    {
        var loginRDto = await manager.AuthService.LoginAsync(loginRequest);
        return loginRDto;
    }

    [HttpPost("register")]
    public async Task<ActionResult<LoginSuccess>> Register([FromForm] RegisterRequest registerRequest)
    {
        var loginRDto = await manager.AuthService.RegisterAsync(registerRequest);
        return Ok(loginRDto);
    }

}

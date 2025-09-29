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
        return await manager.Auth.LoginAsync(loginRequest);
    }

    [HttpPost("register")]
    public async Task<LoginSuccess> Register([FromForm] RegisterRequest registerRequest)
    {
        return await manager.Auth.RegisterAsync(registerRequest);
    }

}

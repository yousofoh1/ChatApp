using Core.Interfaces;
using Core.Dtos.Auth;
using Core.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    public class AuthController(IServicesUOW manager) : BaseController
    {
        // GET: api/<AuthController>
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AuthController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthController>
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

        // PUT api/<AuthController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

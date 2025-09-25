using Core.Contracts;
using Core.Dtos.Requests;
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IServiceManager manager) : ControllerBase
    {
        // GET: api/<AuthController>
        [HttpGet]
        public IEnumerable<string> Get()
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
        public async Task<ActionResult<LoginRDto>> Login([FromBody] LoginRequest loginRequest)
        {
            var loginRDto = await manager.AuthService.LoginAsync(loginRequest);
            return Ok(loginRDto);
        }

        // POST api/<AuthController>
        [HttpPost("register")]
        public async Task<ActionResult<LoginRDto>> Register([FromBody] RegisterRequest registerRequest)
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

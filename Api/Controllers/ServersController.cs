using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    
    public class ServersController : BaseController
    {
        // GET: api/<ServersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ServersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ServersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ServersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ServersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

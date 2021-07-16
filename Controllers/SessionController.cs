using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace alison_etl_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        // GET: api/Session
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Session/5
        [HttpGet("{id}", Name = "GetSession")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Session
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Session/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Session/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

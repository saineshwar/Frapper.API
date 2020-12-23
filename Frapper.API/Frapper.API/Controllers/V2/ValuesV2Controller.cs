using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frapper.API.Filters;
using Frapper.Common;
using Microsoft.AspNetCore.Authorization;

namespace Frapper.API.Controllers.V2
{
    [Authorize(Roles = "User")]
    [Route("api/values")]
    [ApiVersion("2.0")]
    [ApiController]
    public class ValuesV2Controller : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        [MapToApiVersion("2.0")]
        public IActionResult Get()
        {
            var output = new string[] { "value1", "value2" };
            return Ok(output);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        [MapToApiVersion("2.0")]
        public IActionResult Get(int id)
        {
            return Ok(new OkResponse("value"));
        }

        // POST api/<ValuesController>
        [HttpPost]
        [MapToApiVersion("2.0")]
        public IActionResult Post([FromBody] string value)
        {
            return Ok(new OkResponse(value));
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        [MapToApiVersion("2.0")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return Ok(new OkResponse("value"));
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        [MapToApiVersion("2.0")]
        public IActionResult Delete(int id)
        {
            return Ok(new OkResponse(id.ToString()));
        }
    }
}

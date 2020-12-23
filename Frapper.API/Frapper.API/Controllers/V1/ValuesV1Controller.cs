using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Frapper.Common;

namespace Frapper.API.Controllers.V1
{
    //Disabling API Documenation [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize (Roles = "User")]
    [Route("api/values")]
    [ApiVersion("1.0",Deprecated = true)]
    [ApiController]
    public class ValuesV1Controller : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        [MapToApiVersion("1.0")]
        public IActionResult Get()
        {
            var output = new string[] {"value1", "value2"};
            return Ok(output);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        public IActionResult Get(int id)
        {
            return Ok(new OkResponse("value"));
        }

        // POST api/<ValuesController>
        [HttpPost]
        [MapToApiVersion("1.0")]
        public IActionResult Post([FromBody] string value)
        {
            return Ok(new OkResponse(value));
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return Ok(new OkResponse("value"));
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public IActionResult Delete(int id)
        {
            return Ok(new OkResponse(id.ToString()));
        }
    }
}

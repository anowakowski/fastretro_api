using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FirebaseAdmin;
using FirebaseAdmin.Auth;

namespace Fastretro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateFTController : ControllerBase
    {

        // GET: api/GenerateFT
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var uid = "xLWFPyobY1YclPScgErROgHVsGW2";
            string customToken = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(uid.ToString());
            return new string[] { "value1", "value2" };
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> Login(int uid)
        {
            try
            {
                string customToken = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(uid.ToString());

                return this.Ok(customToken);
            }
            catch (Exception e)
            {
                return this.StatusCode(401, new { Error = e.Message });
            }
        }

        [HttpPost("{id}/setCurrentUser")]
        public async Task<IActionResult> SetUpCurrentUser(string id)
        {
            try
            {
                string customToken = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(id);
                return Ok(customToken);
            }
            catch (Exception)
            {
                return BadRequest("Can't unfollow that user");
            }
        }

        // GET: api/GenerateFT/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GenerateFT
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/GenerateFT/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

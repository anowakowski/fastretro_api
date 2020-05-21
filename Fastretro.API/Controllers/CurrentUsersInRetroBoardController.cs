using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fastretro.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fastretro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentUsersInRetroBoardController : ControllerBase
    {
        private readonly ICurrentUsersInRetroBoardServices currentUsersInRetroBoardServices;

        public CurrentUsersInRetroBoardController(ICurrentUsersInRetroBoardServices currentUsersInRetroBoardServices)
        {
            this.currentUsersInRetroBoardServices = currentUsersInRetroBoardServices;
        }

        // GET: api/CurrentUserInRetroBoard
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CurrentUserInRetroBoard/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            this.currentUsersInRetroBoardServices.GetCurrentUsersInRetroBoard("test", id.ToString());
            return "value";
        }

        [HttpPost]
        public async Task<IActionResult> SetUpCurrentUser(string docUserId, string retroBoardId)
        {
            try
            {
                await Task.Run(() => this.currentUsersInRetroBoardServices.SetUpCurrentUserInRetroBoard(docUserId, retroBoardId));

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Can't unfollow that user");
            }
        }

        // POST: api/CurrentUserInRetroBoard
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/CurrentUserInRetroBoard/5
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

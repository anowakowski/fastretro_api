using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fastretro.API.Models;
using Fastretro.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fastretro.API.Controllers
{
    [Authorize]
    //[Route("api/forCurrentUsersInRetroBoard/{retroBoardId}/RetroBoards")]
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentUsersInRetroBoardController : ControllerBase
    {
        private readonly ICurrentUsersInRetroBoardServices currentUsersInRetroBoardServices;

        public CurrentUsersInRetroBoardController(ICurrentUsersInRetroBoardServices currentUsersInRetroBoardServices)
        {
            this.currentUsersInRetroBoardServices = currentUsersInRetroBoardServices;
        }

        [HttpGet("getCurrentUserInRetroBoard/{retroBoardId}")]
        public async Task<IActionResult> Get(string retroBoardId)
        {
            try
            {
                return Ok(await this.currentUsersInRetroBoardServices.GetCurrentUsersInRetroBoard(retroBoardId));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("setCurrentUser")]
        public async Task<IActionResult> SetUpCurrentUserProcess([FromBody] CurrentUserDataModel currentUserDataModel)
        {
            try
            {
                await Task.Run(() => this.currentUsersInRetroBoardServices.SetUpCurrentUserInRetroBoard(currentUserDataModel.UserId, currentUserDataModel.RetroBoardId));

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

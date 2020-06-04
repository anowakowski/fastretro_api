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
        private readonly IFreshCurrentUserInRetroBoardServices freshCurrentUserInRetroBoardServices;
        private readonly ICurrentUserVoteServices currentUserVoteServices;

        public CurrentUsersInRetroBoardController(
            ICurrentUsersInRetroBoardServices currentUsersInRetroBoardServices,
            IFreshCurrentUserInRetroBoardServices freshCurrentUserInRetroBoardServices,
            ICurrentUserVoteServices currentUserVoteServices)
        {
            this.currentUsersInRetroBoardServices = currentUsersInRetroBoardServices;
            this.freshCurrentUserInRetroBoardServices = freshCurrentUserInRetroBoardServices;
            this.currentUserVoteServices = currentUserVoteServices;
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
                await Task.Run(() => this.currentUsersInRetroBoardServices.SetUpCurrentUserInRetroBoard(currentUserDataModel));

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Can't unfollow that user");
            }
        }

        [HttpPost("prepareFreshListOfCurrentUsers")]
        public async Task<IActionResult> PrepareFreshListOfCurrentUsers([FromBody] GetFreshCurrentUserDataModel model)
        {
            try
            {
                await Task.Run(() => this.freshCurrentUserInRetroBoardServices.SetUpFreshListOfCurrentUsers(model));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Can't remove user");
            }
        }

        [HttpPost("setUserVote")]
        public async Task<IActionResult> AddCurentUserVote([FromBody] CurrentUserVoteModel model)
        {
            try
            {
                await Task.Run(() => this.currentUserVoteServices.AddUserVote(model));

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Can't unfollow that user");
            }
        }

        [HttpPost("removeUserVote")]
        public async Task<IActionResult> RemoveCurrentUserVote([FromBody] CurrentUserVoteModel model)
        {
            try
            {
                await Task.Run(() => this.currentUserVoteServices.RemoveUserVote(model));

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Can't unfollow that user");
            }
        }

        [HttpPost("removeUserVoteWhenMergedCard")]
        public async Task<IActionResult> RemoveCurrentUserVoteForMerge([FromBody] CurrentUserVoteModelForMerge model)
        {
            try
            {
                await Task.Run(() => this.currentUserVoteServices.RemoveUserVoteForMerge(model));

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Can't unfollow that user");
            }
        }

        [HttpGet("getUsersVote/{retroBoardId}")]
        public async Task<IActionResult> GetUsersVotes(string retroBoardId)
        {
            try
            {
                return Ok(await this.currentUserVoteServices.GetCurrentUserVoteInRetroBoard(retroBoardId));
            }
            catch (Exception)
            {
                return BadRequest("Can't unfollow that user");
            }
        }

        [HttpGet("getUserVoteCount/{retroBoardId}/{userId}")]
        public async Task<IActionResult> CheckUserVotes(string retroBoardId, string userId)
        {
            try
            {
                return Ok(await this.currentUserVoteServices.GetUserVoteCount(retroBoardId, userId));
            }
            catch (Exception)
            {
                return BadRequest("Can't unfollow that user");
            }
        }

    }
}

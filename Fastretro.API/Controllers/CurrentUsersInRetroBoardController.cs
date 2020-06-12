using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fastretro.API.Models;
using Fastretro.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Fastretro.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentUsersInRetroBoardController : ControllerBase
    {
        private readonly ICurrentUsersInRetroBoardServices currentUsersInRetroBoardServices;
        private readonly IFreshCurrentUserInRetroBoardServices freshCurrentUserInRetroBoardServices;
        private readonly ICurrentUserVoteServices currentUserVoteServices;
        private readonly IRetroBoardOptionServices retroBoardOptionServices;
        private readonly IRetroBoardAdditionalInfoServices retroBoardAdditionalInfoServices;
        private readonly IUsersInTeamServices usersInTeamServices;

        public CurrentUsersInRetroBoardController(
            ICurrentUsersInRetroBoardServices currentUsersInRetroBoardServices,
            IFreshCurrentUserInRetroBoardServices freshCurrentUserInRetroBoardServices,
            ICurrentUserVoteServices currentUserVoteServices,
            IRetroBoardOptionServices retroBoardOptionServices,
            IRetroBoardAdditionalInfoServices retroBoardAdditionalInfoServices,
            IUsersInTeamServices usersInTeamServices)
        {
            this.currentUsersInRetroBoardServices = currentUsersInRetroBoardServices;
            this.freshCurrentUserInRetroBoardServices = freshCurrentUserInRetroBoardServices;
            this.currentUserVoteServices = currentUserVoteServices;
            this.retroBoardOptionServices = retroBoardOptionServices;
            this.retroBoardAdditionalInfoServices = retroBoardAdditionalInfoServices;
            this.usersInTeamServices = usersInTeamServices;
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


        [HttpPost("setRetroBoardOptions")]
        public async Task<IActionResult> SetRetroBoardOptions([FromBody] RetroBoardOptionsModel model)
        {
            try
            {
                await Task.Run(() => this.retroBoardOptionServices.SetRetroBoardOptions(model));

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Can't unfollow that user");
            }
        }

        [HttpGet("getRetroBoardOptions/{retroBoardId}")]
        public async Task<IActionResult> GetRetroBoardOptions(string retroBoardId)
        {
            try
            {
                return Ok(await this.retroBoardOptionServices.GetRetroBoardOptions(retroBoardId));
            }
            catch (Exception)
            {
                return BadRequest("Can't get retro board options");
            }
        }

        [HttpPost("setRetroBoardAdditionalInfo")]
        public async Task<IActionResult> SetRetroBoardAdditionalInfo([FromBody] RetroBoardAdditionalInfoModel model)
        {
            try
            {
                await Task.Run(() => this.retroBoardAdditionalInfoServices.SetRetroBoardAdditionalInfo(model));

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Can't unfollow that user");
            }
        }

        [HttpGet("getRetroBoardAdditionalInfo/{retroBoardId}")]
        public async Task<IActionResult> GetRetroBoardAdditionalInfo(string retroBoardId)
        {
            try
            {
                return Ok(await this.retroBoardAdditionalInfoServices.GetRetroBoardAdditionalInfo(retroBoardId));
            }
            catch (Exception)
            {
                return BadRequest("Can't get retro board options");
            }
        }

        [HttpGet("getPreviousIdOfRetroBoard/{retroBoardId}/{workspaceId}/{teamId}")]
        public async Task<IActionResult> GetRetroBoardAdditionalInfoPreviousRbId(string retroBoardId, string workspaceId, string teamId)
        {
            try
            {
                return Ok(await this.retroBoardAdditionalInfoServices.GetRetroBoardAdditionalInfoPreviousRbId(retroBoardId, teamId, workspaceId));
            }
            catch (Exception)
            {
                return BadRequest("Can't get retro board options");
            }
        }


        [HttpPost("setRetroBoardAdditionalInfoWithActionCount")]
        public async Task<IActionResult> SetRetroBoardAdditionalInfoRetroBoardActionCount([FromBody] RetroBoardAdditionalInfoRetroBoardActionCountModel model)
        {
            try
            {
                await Task.Run(() => this.retroBoardAdditionalInfoServices.SetRetroBoardAdditionalInfoRetroBoardActionCount(model));

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Can't unfollow that user");
            }
        }

        [HttpPost("setUserInTeam")]
        public async Task<IActionResult> SetUserInTeam([FromBody] UsersInTeamModel model)
        {
            try
            {
                await Task.Run(() => this.usersInTeamServices.SetUserInTeam(model));

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Can't unfollow that user");
            }
        }

        [HttpGet("GetUsersInTeam/{workspaceFirebaseDocId}/{teamFirebaseDocId}")]
        public async Task<IActionResult> GetUsersInTeam(string workspaceFirebaseDocId, string teamFirebaseDocId)
        {
            try
            {
                UsersInTeamToGetModel model = new UsersInTeamToGetModel
                {
                    WorkspaceFirebaseDocId = workspaceFirebaseDocId,
                    TeamFirebaseDocId = teamFirebaseDocId
                };

                return Ok(await this.usersInTeamServices.GetUsersInTeam(model));
            }
            catch (Exception)
            {
                return BadRequest("Can't get retro board options");
            }
        }
    }
}

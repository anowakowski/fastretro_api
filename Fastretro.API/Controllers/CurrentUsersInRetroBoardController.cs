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
        private readonly IUsersInActionServices usersInActionServices;
        private readonly IRetroBoardStatusServices retroBoardStatusServices;
        private readonly IUserNotificationServices userNotificationServices;
        private readonly IUserWaitingToApproveWorkspaceJoinServices userWaitingToApproveWorkspaceJoinServices;

        public CurrentUsersInRetroBoardController(
            ICurrentUsersInRetroBoardServices currentUsersInRetroBoardServices,
            IFreshCurrentUserInRetroBoardServices freshCurrentUserInRetroBoardServices,
            ICurrentUserVoteServices currentUserVoteServices,
            IRetroBoardOptionServices retroBoardOptionServices,
            IRetroBoardAdditionalInfoServices retroBoardAdditionalInfoServices,
            IUsersInTeamServices usersInTeamServices,
            IUsersInActionServices usersInActionServices,
            IRetroBoardStatusServices retroBoardStatusServices,
            IUserNotificationServices userNotificationServices,
            IUserWaitingToApproveWorkspaceJoinServices userWaitingToApproveWorkspaceJoinServices)
        {
            this.currentUsersInRetroBoardServices = currentUsersInRetroBoardServices;
            this.freshCurrentUserInRetroBoardServices = freshCurrentUserInRetroBoardServices;
            this.currentUserVoteServices = currentUserVoteServices;
            this.retroBoardOptionServices = retroBoardOptionServices;
            this.retroBoardAdditionalInfoServices = retroBoardAdditionalInfoServices;
            this.usersInTeamServices = usersInTeamServices;
            this.usersInActionServices = usersInActionServices;
            this.retroBoardStatusServices = retroBoardStatusServices;
            this.userNotificationServices = userNotificationServices;
            this.userWaitingToApproveWorkspaceJoinServices = userWaitingToApproveWorkspaceJoinServices;
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

        [HttpPost("setUserInAction")]
        public async Task<IActionResult> SetUserInAction([FromBody] UsersInActionModel model)
        {
            try
            {
                await Task.Run(() => this.usersInActionServices.SetUserInAction(model));

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Can't unfollow that user");
            }
        } 

        [HttpGet("getUsersInActionForRetroBoardCard/{teamFirebaseDocId}/{workspaceFirebaseDocId}/{retroBoardCardFirebaseDocId}/{retroBoardActionCardFirebaseDocId}")]
        public async Task<IActionResult> GetUsersInActionForRetroBoardCard(string teamFirebaseDocId, string workspaceFirebaseDocId, string retroBoardCardFirebaseDocId, string retroBoardActionCardFirebaseDocId)
        {
            try
            {
                return Ok(await this.usersInActionServices.GetUserInActionForRetroBoardCard(teamFirebaseDocId, workspaceFirebaseDocId, retroBoardCardFirebaseDocId, retroBoardActionCardFirebaseDocId));
            }
            catch (Exception)
            {
                return BadRequest("Can't get retro board options");
            }
        }

        [HttpGet("getUsersInAction/{workspaceFirebaseDocId}/{teamFirebaseDocId}")]
        public async Task<IActionResult> GetUsersInActionForTeamInWorksapace(string workspaceFirebaseDocId, string teamFirebaseDocId)
        {
            try
            {
                return Ok(await this.usersInActionServices.GetUsersInActionForTeam(teamFirebaseDocId, workspaceFirebaseDocId));
            }
            catch (Exception)
            {
                return BadRequest("Can't get retro board options");
            }
        }

        [HttpPost("setLastRetroBoard")]
        public async Task<IActionResult> SetLastRetroBoard([FromBody] RetroBoardStatusModel model)
        {
            try
            {
                await Task.Run(() => this.retroBoardStatusServices.SetNewRetroBoardStatus(model));

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Can't unfollow that user");
            }
        }

        [HttpPost("setRetroBoardAsStarted")]
        public async Task<IActionResult> SetRetroBoardAsStarted([FromBody] RetroBoardStatusForSetRBAsStartedModel model)
        {
            try
            {
                await Task.Run(() => this.retroBoardStatusServices.SetRetroBoardAsStarted(model));

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Can't unfollow that user");
            }
        }

        [HttpPost("setRetroBoardAsFinished")]
        public async Task<IActionResult> SetRetroBoardAsFinished([FromBody] RetroBoardStatusForSetRBAsFinishedModel model)
        {
            try
            {
                await Task.Run(() => this.retroBoardStatusServices.SetRetroBoardAsFinished(model));

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Can't unfollow that user");
            }
        }
 
        [HttpPost("setRetroBoardAsOpened")]
        public async Task<IActionResult> SetRetroBoardAsOpened([FromBody] RetroBoardStatusForSetRBAsOpenedModel model)
        {
            try
            {
                await Task.Run(() => this.retroBoardStatusServices.SetRetroBoardAsOpened(model));

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Can't unfollow that user");
            }
        }                 
        
        [HttpGet("getUsersLastRetroBoardsStatus/{workspaceFirebaseDocId}")]
        public async Task<IActionResult> GetUsersLastRetroBoardsStatus(string workspaceFirebaseDocId)
        {
            try
            {
                return Ok(await this.retroBoardStatusServices.GetLastRetroBoardForWorkspace(workspaceFirebaseDocId));
            }
            catch (Exception)
            {
                return BadRequest("Can't get retro board options");
            }
        }

        [HttpPost("setUserNotification")]
        public async Task<IActionResult> SetUserNotification([FromBody] UserNotificationModel model)
        {
            try
            {
                await Task.Run(() => this.userNotificationServices.SetUserNotification(model));
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Can't set new notification");
            }
        }

        [HttpPost("setApproveUserWantToJoinToWorkspace")]
        public async Task<IActionResult> SetApproveUserWantToJoinToWorkspace([FromBody] UserWaitingToApproveWorkspaceJoinModel model)
        {
            try
            {
                await Task.Run(() => this.userWaitingToApproveWorkspaceJoinServices.SetApproveUserWantToJoinToWorkspace(model));
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Can't unfollow that user");
            }
        }

        [HttpPost("setUserNotificationAsRead")]
        public async Task<IActionResult> SetUserNotificationAsRead([FromBody] UserNotificationAsReadModel model)
        {
            try
            {
                await Task.Run(() => this.userNotificationServices.SetUserNotificationAsRead(model));
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Can't unfollow that user");
            }
        }

        [HttpGet("getUserNotifications/{userId}")]
        public async Task<IActionResult> GetUserNotifications(string userId)
        {
            try
            {
                return Ok(await this.userNotificationServices.GetUserNotification(userId));
            }
            catch (Exception)
            {
                return BadRequest("Can't get retro board options");
            }
        }

        [HttpGet("getUserWaitingToApproveWorkspaceJoin/{userWantToJoinFirebaseId}/{creatorUserFirebaseId}/{workspceWithRequiredAccessFirebaseId}/{userWaitingToApproveWorkspaceJoinId}")]
        public async Task<IActionResult> GetUserWaitingToApproveWorkspaceJoin(
            string userWantToJoinFirebaseId,
            string creatorUserFirebaseId,
            string workspceWithRequiredAccessFirebaseId,
            int userWaitingToApproveWorkspaceJoinId)
        {
            try
            {
                return Ok(await this.userWaitingToApproveWorkspaceJoinServices.GetUserWaitingToApproveWorkspaceJoin(userWantToJoinFirebaseId, creatorUserFirebaseId, workspceWithRequiredAccessFirebaseId, userWaitingToApproveWorkspaceJoinId));
            }
            catch (Exception)
            {
                return BadRequest("Can't get retro board options");
            }
        }
    }
}

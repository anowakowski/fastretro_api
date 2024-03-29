﻿using Fastretro.API.Data;
using Fastretro.API.Data.Domain;
using Fastretro.API.Data.Repositories;
using Fastretro.API.Migrations;
using Fastretro.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Services
{
    public class UsersInTeamServices : IUsersInTeamServices
    {
        private readonly IRepository<UsersInTeam> repository;
        private readonly IUnitOfWork unitOfWork;

        public UsersInTeamServices(IRepository<UsersInTeam> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UsersInTeam>> GetUsersInTeam(UsersInTeamToGetModel model)
        {
            bool isExistingTeamWithUsers =
                await this.repository.AnyAsync(u =>
                    u.TeamFirebaseDocId == model.TeamFirebaseDocId &&
                    u.WorkspaceFirebaseDocId == model.WorkspaceFirebaseDocId);

            var findedUsersInTeam = await this.repository.FindAsync(u =>
                                            u.TeamFirebaseDocId == model.TeamFirebaseDocId &&
                                            u.WorkspaceFirebaseDocId == model.WorkspaceFirebaseDocId);

            return findedUsersInTeam.ToList();
        }

        public async Task RemoveUserInTeam(IEnumerable<UsersInTeamRemoveModel> model)
        {
            foreach(var teamToLeave in model)
            {
                var findedUserInTeams = await this.repository.FirstOrDefaultAsync(uit =>
                    uit.TeamFirebaseDocId == teamToLeave.TeamFirebaseDocId &&
                    uit.UserFirebaseDocId == teamToLeave.UserFirebaseDocId &&
                    uit.WorkspaceFirebaseDocId == teamToLeave.WorkspaceFirebaseDocId);

                this.repository.Delete(findedUserInTeams);
                await this.unitOfWork.CompleteAsync();
            }
        }

        public async Task SetUserInTeam(UsersInTeamModel model)
        {
            bool isExistingUserWithTeam = await IsExistingUserInTeams(model);

            if (!isExistingUserWithTeam)
            {
                UsersInTeam usersInTeam = new UsersInTeam
                {
                    UserFirebaseDocId = model.UserFirebaseDocId,
                    TeamFirebaseDocId = model.TeamFirebaseDocId,
                    WorkspaceFirebaseDocId = model.WorkspaceFirebaseDocId,
                    ChosenAvatarName = model.ChosenAvatarName,
                    DisplayName = model.DisplayName
                };

                await this.repository.AddAsync(usersInTeam);
                await this.unitOfWork.CompleteAsync();
            }
        }

        private async Task<bool> IsExistingUserInTeams(UsersInTeamModel model)
        {
            return await this.repository.AnyAsync(u =>
                    u.UserFirebaseDocId == model.UserFirebaseDocId &&
                    u.TeamFirebaseDocId == model.TeamFirebaseDocId &&
                    u.WorkspaceFirebaseDocId == model.WorkspaceFirebaseDocId);
        }
    }
}

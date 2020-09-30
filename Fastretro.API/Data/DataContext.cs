using Fastretro.API.Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace Fastretro.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<CurrentUserInRetroBoard> CurrentUserInRetroBoards { get; set; }
        public DbSet<FirebaseUserData> FirebaseUsersData { get; set; }
        public DbSet<CurrentUserVote> CurrentUserVotes { get; set; }
        public DbSet<RetroBoardOptions> RetroBoardOptions { get; set; }
        public DbSet<RetroBoardAdditionalInfo> RetroBoardAdditionalInfos { get; set; }
        public DbSet<UsersInTeam> UsersInTeams { get; set; }
        public DbSet<UsersInAction> UsersInActions { get; set; }
        public DbSet<RetroBoardStatus> RetroBoardStatuses { get; set; }
        public DbSet<UserNotification> userNotifications { get; set; }
        public DbSet<UserNotificationWorkspaceWithRequiredAccess> UserNotificationWorkspaceWithRequiredAccesses { get; set; }
        public DbSet<UserWaitingToApproveWorkspaceJoin> userWaitingToApproveWorkspaceJoins { get; set; }
        public DbSet<UserNotificationWorkspaceWithRequiredAccessResponse> UserNotificationWorkspaceWithRequiredAccessResponses { get; set; }
        public DbSet<RetroBoard> RetroBoards { get; set; }
        public DbSet<RetroBoardCard> RetroBoardCards { get; set; }
        public DbSet<MergedRetroBoardCard> MergedRetroBoardCards { get; set; }
        public DbSet<RetroBoardCardMergedGroup> RetroBoardCardMergedGroups { get; set; }
        public DbSet<RetroBoardActionCard> RetroBoardActionCards { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
    }
}

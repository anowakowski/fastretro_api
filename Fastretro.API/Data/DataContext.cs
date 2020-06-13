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
    }
}

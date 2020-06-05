using Fastretro.API.Data.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<CurrentUserInRetroBoard> CurrentUserInRetroBoards { get; set; }

        public DbSet<FirebaseUserData> FirebaseUsersData { get; set; }

        public DbSet<CurrentUserVote> CurrentUserVotes { get; set; }
        
        public DbSet<RetroBoardOptions> RetroBoardOptions { get; set; }
    }
}

using Frapper.Entities.Movies;
using Frapper.Entities.Rolemasters;
using Frapper.Entities.UserMaster;
using Microsoft.EntityFrameworkCore;

namespace Frapper.Repository
{
    public class FrapperDbContext : DbContext
    {
        public FrapperDbContext(DbContextOptions<FrapperDbContext> options) : base(options)
        {

        }

        public DbSet<Entities.UserMaster.UserMaster> UserMasters { get; set; }
        public DbSet<AssignedRoles> AssignedRoles { get; set; }
        public DbSet<UserTokens> UserTokens { get; set; }
        public DbSet<RoleMaster> RoleMasters { get; set; }
        public DbSet<Entities.Movies.Movies> Movies { get; set; }
    }
}
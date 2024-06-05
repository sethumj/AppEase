using Microsoft.EntityFrameworkCore;
namespace AppEase.Models
{
    public class AppEaseDbContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public AppEaseDbContext(DbContextOptions<AppEaseDbContext> options) : base(options) { }
    }
}

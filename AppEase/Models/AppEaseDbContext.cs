using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace AppEase.Models
{
    public class AppEaseDbContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public AppEaseDbContext(DbContextOptions<AppEaseDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>()
                .HasOne(j => j.Profile)
                .WithMany(p => p.Jobs)
                .HasForeignKey(j => j.ProfileId);
        }
    }
}

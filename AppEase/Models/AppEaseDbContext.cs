using AppEase.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace AppEase.Models
{
    public class AppEaseDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Address> Address { get; set; } 
        public AppEaseDbContext(DbContextOptions<AppEaseDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Job>()
                .HasOne(j => j.Profile)
                .WithMany(p => p.Jobs)
                .HasForeignKey(j => j.ProfileId);

        }
    }
}

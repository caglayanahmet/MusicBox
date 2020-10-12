using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace MusicBox.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followings { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>()
                .HasRequired(a=>a.Event)
                .WithMany()
                .WillCascadeOnDelete(false);
            
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(a=>a.Followers)
                .WithRequired(a=>a.Followee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(a=>a.Followees)
                .WithRequired(a=>a.Follower)
                .WillCascadeOnDelete(false);
        }
    }
}
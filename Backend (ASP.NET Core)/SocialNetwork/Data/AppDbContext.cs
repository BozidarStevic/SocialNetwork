using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Models;
using static System.Net.Mime.MediaTypeNames;

namespace SocialNetwork.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<FollowingUser> FollowingUsers { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public new DbSet<User> Users { get; set; }
        public new DbSet<IdentityRole> Roles { get; set; }
        public DbSet<View> Views { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FollowingUser>()
                .HasKey(fu => new { fu.FollowerId, fu.FollowedUserId });

            modelBuilder.Entity<FollowingUser>()
                .HasOne(fu => fu.Follower)
                .WithMany(u => u.Followers)
                .HasForeignKey(fu => fu.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FollowingUser>()
                .HasOne(fu => fu.FollowedUser)
                .WithMany(u => u.Following)
                .HasForeignKey(fu => fu.FollowedUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}

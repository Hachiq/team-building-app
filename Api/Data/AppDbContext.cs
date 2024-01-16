using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<Team> Teams => Set<Team>();
        public DbSet<Request> Requests => Set<Request>();
        public DbSet<Stats> Stats => Set<Stats>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CS_AS");

            modelBuilder.Entity<UserRole>()
             .HasKey(ur => new { ur.UserId, ur.RoleId });
        }

    }
}

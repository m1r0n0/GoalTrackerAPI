using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
                    : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Goal> GoalList { get; set; } = default!;
        public DbSet<Member> MembersIds { get; set; } = default!;
        public DbSet<GoalTask> GoalTasks { get; set; } = default!;
        public DbSet<User> UserList { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Goal>().HasIndex(u => new { u.MainGoalId });
        }
    }
}

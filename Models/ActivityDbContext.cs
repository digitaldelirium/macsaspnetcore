using Microsoft.EntityFrameworkCore;

namespace MacsASPNETCore.Models
{
    public class ActivityDbContext : DbContext
    {
        public ActivityDbContext(DbContextOptions<ActivityDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<Calendar> Calendars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>()
                .ForSqlServerIsMemoryOptimized()
                .Property(a => a.StartTime)
                .IsRequired();

            modelBuilder.Entity<Calendar>()
                .Property(c => c.Year)
                .IsRequired();
        }
    }
}
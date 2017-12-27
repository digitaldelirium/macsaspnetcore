using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.PlatformAbstractions;

namespace MacsASPNETCore.Models
{
    public class ReservationDbContext : DbContext   
    {
        public ReservationDbContext()
        {
            Database.EnsureCreated();
        }
         public DbSet<Reservation> Reservations {get; set;}
         
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var path = PlatformServices.Default.Application.ApplicationBasePath;
            optionsBuilder.UseSqlite("Filename=" + Path.Combine(path, "Reservations.db"));
            
            base.OnConfiguring(optionsBuilder);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>()
                .Property(r => r.Id)
                .IsRequired();
                
            modelBuilder.Entity<Reservation>()
                .Property(r => r.SiteType)
                .IsRequired();
                
            modelBuilder.Entity<Reservation>()
                .Property(r => r.CustomerId)
                .IsRequired();
        }
    }
}
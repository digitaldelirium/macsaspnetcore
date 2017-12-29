using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.PlatformAbstractions;

namespace MacsASPNETCore.Models
{
    public class ReservationDbContext : DbContext   
    {
        public ReservationDbContext(DbContextOptions<ReservationDbContext> options): base(options)
        {
            Database.EnsureCreated();
        }
        
        public DbSet<Reservation> Reservations {get; set;}
         
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
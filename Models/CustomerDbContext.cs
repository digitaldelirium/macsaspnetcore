using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.PlatformAbstractions;

namespace MacsASPNETCore.Models
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(){
            Database.EnsureCreated();
        }
        public DbSet<Customer> Customers { get; set; } 
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Postal> PostalCodes { get; set; }
        public DbSet<Email> EmailAddresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var path = PlatformServices.Default.Application.ApplicationBasePath;
            optionsBuilder.UseSqlite("Filename=" + Path.Combine(path, "Customers.db"));
            
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(c => c.FirstName)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(c => c.LastName)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(c => c.EmailId)
                .IsRequired();
        }
    }
}
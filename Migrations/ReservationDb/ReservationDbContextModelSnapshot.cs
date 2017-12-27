using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MacsASPNETCore.Models;

namespace MacsASPNETCore.Migrations.ReservationDb
{
    [DbContext(typeof(ReservationDbContext))]
    partial class ReservationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("MacsASPNETCore.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<bool>("ReservationConfirmed");

                    b.Property<DateTime>("ReservationDate");

                    b.Property<int>("SiteType");

                    b.HasKey("Id");
                });
        }
    }
}

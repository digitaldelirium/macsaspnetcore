using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MacsASPNETCore.Models;

namespace MacsASPNETCore.Migrations.CustomerDb
{
    [DbContext(typeof(CustomerDbContext))]
    [Migration("20160423202440_AddCustomers")]
    partial class AddCustomers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("MacsASPNETCore.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CityId");

                    b.Property<int>("CountryId");

                    b.Property<int>("CustomerId");

                    b.Property<int>("PostalId");

                    b.Property<int>("StateId");

                    b.Property<string>("StreetAddress");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("MacsASPNETCore.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CountryName");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("MacsASPNETCore.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddressId");

                    b.Property<int>("EmailId");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("MacsASPNETCore.Models.Email", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerId");

                    b.Property<string>("EmailAddress");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("MacsASPNETCore.Models.PhoneNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerId");

                    b.Property<string>("TelNumber")
                        .IsRequired();

                    b.HasKey("Id");
                });

            modelBuilder.Entity("MacsASPNETCore.Models.Postal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CountryId");

                    b.Property<string>("PostalCode");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("MacsASPNETCore.Models.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("StateName");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("MacsASPNETCore.Models.PhoneNumber", b =>
                {
                    b.HasOne("MacsASPNETCore.Models.Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");
                });
        }
    }
}

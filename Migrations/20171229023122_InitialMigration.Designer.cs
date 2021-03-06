﻿// <auto-generated />
using MacsASPNETCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace MacsASPNETCore.Migrations
{
    [DbContext(typeof(ActivityDbContext))]
    [Migration("20171229023122_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("MacsASPNETCore.Models.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActivityDescription");

                    b.Property<string>("ActivityTitle");

                    b.Property<int?>("CalendarId");

                    b.Property<DateTime>("EndTime");

                    b.Property<DateTime>("StartTime");

                    b.HasKey("Id");

                    b.HasIndex("CalendarId");

                    b.ToTable("Activities");

                    b.HasAnnotation("SqlServer:MemoryOptimized", true);
                });

            modelBuilder.Entity("MacsASPNETCore.Models.Calendar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("Calendars");
                });

            modelBuilder.Entity("MacsASPNETCore.Models.Activity", b =>
                {
                    b.HasOne("MacsASPNETCore.Models.Calendar")
                        .WithMany("Activities")
                        .HasForeignKey("CalendarId");
                });
#pragma warning restore 612, 618
        }
    }
}

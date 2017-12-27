using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MacsASPNETCore.Models;

namespace MacsASPNETCore.Migrations.ActivityDb
{
    [DbContext(typeof(ActivityDbContext))]
    [Migration("20160424155214_FixedActivityDataAnnotations")]
    partial class FixedActivityDataAnnotations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

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
                });

            modelBuilder.Entity("MacsASPNETCore.Models.Calendar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Year");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("MacsASPNETCore.Models.Activity", b =>
                {
                    b.HasOne("MacsASPNETCore.Models.Calendar")
                        .WithMany()
                        .HasForeignKey("CalendarId");
                });
        }
    }
}

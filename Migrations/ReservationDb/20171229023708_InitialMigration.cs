using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MacsASPNETCore.Migrations.ReservationDb
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    ReservationConfirmed = table.Column<bool>(nullable: false),
                    ReservationDate = table.Column<DateTime>(nullable: false),
                    SiteType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");
        }
    }
}

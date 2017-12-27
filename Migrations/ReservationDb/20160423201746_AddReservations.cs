using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MacsASPNETCore.Migrations.ReservationDb
{
    public partial class AddReservations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    ReservationConfirmed = table.Column<bool>(nullable: false),
                    ReservationDate = table.Column<DateTime>(nullable: false),
                    SiteType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Reservation");
        }
    }
}

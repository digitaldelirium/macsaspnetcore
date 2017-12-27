using Microsoft.EntityFrameworkCore.Migrations;

namespace MacsASPNETCore.Migrations.CustomerDb
{
    public partial class UpdateCustomersContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_PhoneNumber_Customer_CustomerId", table: "PhoneNumber");
            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumber_Customer_CustomerId",
                table: "PhoneNumber",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_PhoneNumber_Customer_CustomerId", table: "PhoneNumber");
            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumber_Customer_CustomerId",
                table: "PhoneNumber",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

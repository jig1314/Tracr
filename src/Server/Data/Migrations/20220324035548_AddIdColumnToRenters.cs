using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tracr.Server.Data.Migrations
{
    public partial class AddIdColumnToRenters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PrimaryKey_Renter_PropertyId",
                table: "Renters");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Renters",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PrimaryKey_RenterId",
                table: "Renters",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Renters_PropertyId",
                table: "Renters",
                column: "PropertyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PrimaryKey_RenterId",
                table: "Renters");

            migrationBuilder.DropIndex(
                name: "IX_Renters_PropertyId",
                table: "Renters");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Renters");

            migrationBuilder.AddPrimaryKey(
                name: "PrimaryKey_Renter_PropertyId",
                table: "Renters",
                column: "PropertyId");
        }
    }
}

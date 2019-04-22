using Microsoft.EntityFrameworkCore.Migrations;

namespace OLBIL.OncologyData.Migrations
{
    public partial class AddingParentLevelFieldsAdmDivs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "level",
                schema: "olbil",
                table: "administrativedivision",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "parentid",
                schema: "olbil",
                table: "administrativedivision",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_administrativedivision_parentid",
                schema: "olbil",
                table: "administrativedivision",
                column: "parentid");

            migrationBuilder.AddForeignKey(
                name: "FK_administrativedivision_administrativedivision_parentid",
                schema: "olbil",
                table: "administrativedivision",
                column: "parentid",
                principalSchema: "olbil",
                principalTable: "administrativedivision",
                principalColumn: "administrativedivisionid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_administrativedivision_administrativedivision_parentid",
                schema: "olbil",
                table: "administrativedivision");

            migrationBuilder.DropIndex(
                name: "IX_administrativedivision_parentid",
                schema: "olbil",
                table: "administrativedivision");

            migrationBuilder.DropColumn(
                name: "level",
                schema: "olbil",
                table: "administrativedivision");

            migrationBuilder.DropColumn(
                name: "parentid",
                schema: "olbil",
                table: "administrativedivision");
        }
    }
}

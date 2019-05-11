using Microsoft.EntityFrameworkCore.Migrations;

namespace OLBIL.OncologyData.Migrations
{
    public partial class SettingParentLocationIdNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recordstoragelocation_recordstoragelocation_parentlocationid",
                schema: "olbil",
                table: "recordstoragelocation");

            migrationBuilder.AlterColumn<int>(
                name: "parentlocationid",
                schema: "olbil",
                table: "recordstoragelocation",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_recordstoragelocation_recordstoragelocation_parentlocationid",
                schema: "olbil",
                table: "recordstoragelocation",
                column: "parentlocationid",
                principalSchema: "olbil",
                principalTable: "recordstoragelocation",
                principalColumn: "recordstoragelocationid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recordstoragelocation_recordstoragelocation_parentlocationid",
                schema: "olbil",
                table: "recordstoragelocation");

            migrationBuilder.AlterColumn<int>(
                name: "parentlocationid",
                schema: "olbil",
                table: "recordstoragelocation",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_recordstoragelocation_recordstoragelocation_parentlocationid",
                schema: "olbil",
                table: "recordstoragelocation",
                column: "parentlocationid",
                principalSchema: "olbil",
                principalTable: "recordstoragelocation",
                principalColumn: "recordstoragelocationid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

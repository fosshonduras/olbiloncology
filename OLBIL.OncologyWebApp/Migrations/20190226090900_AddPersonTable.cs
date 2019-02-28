using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OLBIL.OncologyWebApp.Migrations
{
    public partial class AddPersonTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OncologyPatients_Person_PersonId",
                table: "OncologyPatients");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_AppUser_AppUserId",
                table: "Person");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUser",
                table: "AppUser");

            migrationBuilder.RenameTable(
                name: "AppUser",
                newName: "AppUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppUserId",
                table: "Person",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "PersonId",
                table: "OncologyPatients",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUsers",
                table: "AppUsers",
                column: "AppUserId");

            migrationBuilder.CreateTable(
                name: "HealthProfessionals",
                columns: table => new
                {
                    HealthProfessionalId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PersonId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthProfessionals", x => x.HealthProfessionalId);
                    table.ForeignKey(
                        name: "FK_HealthProfessionals_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HealthProfessionals_PersonId",
                table: "HealthProfessionals",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_OncologyPatients_Person_PersonId",
                table: "OncologyPatients",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_AppUsers_AppUserId",
                table: "Person",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OncologyPatients_Person_PersonId",
                table: "OncologyPatients");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_AppUsers_AppUserId",
                table: "Person");

            migrationBuilder.DropTable(
                name: "HealthProfessionals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUsers",
                table: "AppUsers");

            migrationBuilder.RenameTable(
                name: "AppUsers",
                newName: "AppUser");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppUserId",
                table: "Person",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PersonId",
                table: "OncologyPatients",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUser",
                table: "AppUser",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OncologyPatients_Person_PersonId",
                table: "OncologyPatients",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_AppUser_AppUserId",
                table: "Person",
                column: "AppUserId",
                principalTable: "AppUser",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OLBIL.OncologyData.Migrations
{
    public partial class AddingSpecialtyToDoctors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bodymassindex",
                schema: "olbil",
                table: "evolutioncard");

            migrationBuilder.AddColumn<int>(
                name: "mainspecialtyid",
                schema: "olbil",
                table: "healthprofessional",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "healthprofessionalmedicalspecialty",
                schema: "olbil",
                columns: table => new
                {
                    healthprofessionalmedicalspecialtyid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    healthprofessionalid = table.Column<int>(nullable: false),
                    medicalspecialtyid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_healthprofessionalmedicalspecialty", x => x.healthprofessionalmedicalspecialtyid);
                    table.ForeignKey(
                        name: "FK_healthprofessionalmedicalspecialty_healthprofessional_healt~",
                        column: x => x.healthprofessionalid,
                        principalSchema: "olbil",
                        principalTable: "healthprofessional",
                        principalColumn: "healthprofessionalid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_healthprofessionalmedicalspecialty_medicalspecialty_medical~",
                        column: x => x.medicalspecialtyid,
                        principalSchema: "olbil",
                        principalTable: "medicalspecialty",
                        principalColumn: "medicalspecialtyid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_healthprofessional_mainspecialtyid",
                schema: "olbil",
                table: "healthprofessional",
                column: "mainspecialtyid");

            migrationBuilder.CreateIndex(
                name: "IX_healthprofessionalmedicalspecialty_healthprofessionalid",
                schema: "olbil",
                table: "healthprofessionalmedicalspecialty",
                column: "healthprofessionalid");

            migrationBuilder.CreateIndex(
                name: "IX_healthprofessionalmedicalspecialty_medicalspecialtyid",
                schema: "olbil",
                table: "healthprofessionalmedicalspecialty",
                column: "medicalspecialtyid");

            migrationBuilder.AddForeignKey(
                name: "FK_healthprofessional_medicalspecialty_mainspecialtyid",
                schema: "olbil",
                table: "healthprofessional",
                column: "mainspecialtyid",
                principalSchema: "olbil",
                principalTable: "medicalspecialty",
                principalColumn: "medicalspecialtyid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_healthprofessional_medicalspecialty_mainspecialtyid",
                schema: "olbil",
                table: "healthprofessional");

            migrationBuilder.DropTable(
                name: "healthprofessionalmedicalspecialty",
                schema: "olbil");

            migrationBuilder.DropIndex(
                name: "IX_healthprofessional_mainspecialtyid",
                schema: "olbil",
                table: "healthprofessional");

            migrationBuilder.DropColumn(
                name: "mainspecialtyid",
                schema: "olbil",
                table: "healthprofessional");

            migrationBuilder.AddColumn<decimal>(
                name: "bodymassindex",
                schema: "olbil",
                table: "evolutioncard",
                nullable: true);
        }
    }
}

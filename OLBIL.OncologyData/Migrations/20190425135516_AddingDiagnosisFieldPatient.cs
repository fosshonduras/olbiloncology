using Microsoft.EntityFrameworkCore.Migrations;

namespace OLBIL.OncologyData.Migrations
{
    public partial class AddingDiagnosisFieldPatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "maindiagnosisid",
                schema: "olbil",
                table: "oncologypatient",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_oncologypatient_maindiagnosisid",
                schema: "olbil",
                table: "oncologypatient",
                column: "maindiagnosisid");

            migrationBuilder.AddForeignKey(
                name: "FK_oncologypatient_diagnosis_maindiagnosisid",
                schema: "olbil",
                table: "oncologypatient",
                column: "maindiagnosisid",
                principalSchema: "olbil",
                principalTable: "diagnosis",
                principalColumn: "diagnosisid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_oncologypatient_diagnosis_maindiagnosisid",
                schema: "olbil",
                table: "oncologypatient");

            migrationBuilder.DropIndex(
                name: "IX_oncologypatient_maindiagnosisid",
                schema: "olbil",
                table: "oncologypatient");

            migrationBuilder.DropColumn(
                name: "maindiagnosisid",
                schema: "olbil",
                table: "oncologypatient");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OLBIL.OncologyData.Migrations
{
    public partial class AddingAmbulatoryAttention : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ambulatoryattentionrecord",
                schema: "olbil",
                columns: table => new
                {
                    ambulatoryattentionrecordid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    healthprofessionalid = table.Column<int>(nullable: false),
                    oncologypatientid = table.Column<int>(nullable: false),
                    isnewpatient = table.Column<bool>(nullable: false),
                    diagnosisid = table.Column<int>(nullable: false),
                    treatmentphase = table.Column<string>(nullable: true),
                    diseaseeventdescription = table.Column<string>(nullable: true),
                    nextappointmentdate = table.Column<DateTime>(nullable: true),
                    date = table.Column<DateTime>(nullable: false),
                    referredto = table.Column<string>(nullable: true),
                    receivedfrom = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ambulatoryattentionrecord", x => x.ambulatoryattentionrecordid);
                    table.ForeignKey(
                        name: "FK_ambulatoryattentionrecord_diagnosis_diagnosisid",
                        column: x => x.diagnosisid,
                        principalSchema: "olbil",
                        principalTable: "diagnosis",
                        principalColumn: "diagnosisid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ambulatoryattentionrecord_healthprofessional_healthprofessi~",
                        column: x => x.healthprofessionalid,
                        principalSchema: "olbil",
                        principalTable: "healthprofessional",
                        principalColumn: "healthprofessionalid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ambulatoryattentionrecord_oncologypatient_oncologypatientid",
                        column: x => x.oncologypatientid,
                        principalSchema: "olbil",
                        principalTable: "oncologypatient",
                        principalColumn: "oncologypatientid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ambulatoryattentionrecord_diagnosisid",
                schema: "olbil",
                table: "ambulatoryattentionrecord",
                column: "diagnosisid");

            migrationBuilder.CreateIndex(
                name: "IX_ambulatoryattentionrecord_healthprofessionalid",
                schema: "olbil",
                table: "ambulatoryattentionrecord",
                column: "healthprofessionalid");

            migrationBuilder.CreateIndex(
                name: "IX_ambulatoryattentionrecord_oncologypatientid",
                schema: "olbil",
                table: "ambulatoryattentionrecord",
                column: "oncologypatientid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ambulatoryattentionrecord",
                schema: "olbil");
        }
    }
}

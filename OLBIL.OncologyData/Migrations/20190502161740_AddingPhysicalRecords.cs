using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OLBIL.OncologyData.Migrations
{
    public partial class AddingPhysicalRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "recordstoragelocation",
                schema: "olbil",
                columns: table => new
                {
                    recordstoragelocationid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(nullable: true),
                    parentlocationid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recordstoragelocation", x => x.recordstoragelocationid);
                    table.ForeignKey(
                        name: "FK_recordstoragelocation_recordstoragelocation_parentlocationid",
                        column: x => x.parentlocationid,
                        principalSchema: "olbil",
                        principalTable: "recordstoragelocation",
                        principalColumn: "recordstoragelocationid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "patientphysicalrecord",
                schema: "olbil",
                columns: table => new
                {
                    patientphysicalrecordid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    oncologypatientid = table.Column<int>(nullable: false),
                    recordstoragelocationid = table.Column<int>(nullable: false),
                    recordnumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patientphysicalrecord", x => x.patientphysicalrecordid);
                    table.ForeignKey(
                        name: "FK_patientphysicalrecord_oncologypatient_oncologypatientid",
                        column: x => x.oncologypatientid,
                        principalSchema: "olbil",
                        principalTable: "oncologypatient",
                        principalColumn: "oncologypatientid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_patientphysicalrecord_recordstoragelocation_recordstoragelo~",
                        column: x => x.recordstoragelocationid,
                        principalSchema: "olbil",
                        principalTable: "recordstoragelocation",
                        principalColumn: "recordstoragelocationid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "physicalrecordtransfer",
                schema: "olbil",
                columns: table => new
                {
                    physicalrecordtransferid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    patientphysicalrecordid = table.Column<int>(nullable: false),
                    targetlocationid = table.Column<int>(nullable: false),
                    deliveredby = table.Column<string>(nullable: true),
                    receivedby = table.Column<string>(nullable: true),
                    date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_physicalrecordtransfer", x => x.physicalrecordtransferid);
                    table.ForeignKey(
                        name: "FK_physicalrecordtransfer_patientphysicalrecord_patientphysica~",
                        column: x => x.patientphysicalrecordid,
                        principalSchema: "olbil",
                        principalTable: "patientphysicalrecord",
                        principalColumn: "patientphysicalrecordid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_physicalrecordtransfer_recordstoragelocation_targetlocation~",
                        column: x => x.targetlocationid,
                        principalSchema: "olbil",
                        principalTable: "recordstoragelocation",
                        principalColumn: "recordstoragelocationid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_patientphysicalrecord_oncologypatientid",
                schema: "olbil",
                table: "patientphysicalrecord",
                column: "oncologypatientid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_patientphysicalrecord_recordstoragelocationid",
                schema: "olbil",
                table: "patientphysicalrecord",
                column: "recordstoragelocationid");

            migrationBuilder.CreateIndex(
                name: "IX_physicalrecordtransfer_patientphysicalrecordid",
                schema: "olbil",
                table: "physicalrecordtransfer",
                column: "patientphysicalrecordid");

            migrationBuilder.CreateIndex(
                name: "IX_physicalrecordtransfer_targetlocationid",
                schema: "olbil",
                table: "physicalrecordtransfer",
                column: "targetlocationid");

            migrationBuilder.CreateIndex(
                name: "IX_recordstoragelocation_parentlocationid",
                schema: "olbil",
                table: "recordstoragelocation",
                column: "parentlocationid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "physicalrecordtransfer",
                schema: "olbil");

            migrationBuilder.DropTable(
                name: "patientphysicalrecord",
                schema: "olbil");

            migrationBuilder.DropTable(
                name: "recordstoragelocation",
                schema: "olbil");
        }
    }
}

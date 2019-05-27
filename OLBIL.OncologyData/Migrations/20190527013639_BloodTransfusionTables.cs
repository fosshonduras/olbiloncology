using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OLBIL.OncologyData.Migrations
{
    public partial class BloodTransfusionTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bloodtransfusion",
                schema: "olbil",
                columns: table => new
                {
                    bloodtransfusionid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    oncologypatientid = table.Column<int>(nullable: false),
                    wardid = table.Column<int>(nullable: false),
                    verifyby = table.Column<string>(nullable: true),
                    date = table.Column<DateTime>(nullable: false),
                    group = table.Column<string>(nullable: true),
                    aborh = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bloodtransfusion", x => x.bloodtransfusionid);
                    table.ForeignKey(
                        name: "FK_bloodtransfusion_oncologypatient_oncologypatientid",
                        column: x => x.oncologypatientid,
                        principalSchema: "olbil",
                        principalTable: "oncologypatient",
                        principalColumn: "oncologypatientid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bloodtransfusion_ward_wardid",
                        column: x => x.wardid,
                        principalSchema: "olbil",
                        principalTable: "ward",
                        principalColumn: "wardid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transfusionphase",
                schema: "olbil",
                columns: table => new
                {
                    transfusionphaseid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transfusionphase", x => x.transfusionphaseid);
                });

            migrationBuilder.CreateTable(
                name: "transfusionproductdetail",
                schema: "olbil",
                columns: table => new
                {
                    transfusionproductdetailid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    bloodtransfusionid = table.Column<int>(nullable: false),
                    unitnumber = table.Column<string>(nullable: true),
                    component = table.Column<string>(nullable: true),
                    quantity = table.Column<decimal>(nullable: false),
                    aborh = table.Column<string>(nullable: true),
                    starttime = table.Column<DateTime>(nullable: false),
                    endtime = table.Column<DateTime>(nullable: true),
                    responsible = table.Column<string>(nullable: true),
                    adversereactions = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transfusionproductdetail", x => x.transfusionproductdetailid);
                    table.ForeignKey(
                        name: "FK_transfusionproductdetail_bloodtransfusion_bloodtransfusionid",
                        column: x => x.bloodtransfusionid,
                        principalSchema: "olbil",
                        principalTable: "bloodtransfusion",
                        principalColumn: "bloodtransfusionid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transfusionvitalsignsdetail",
                schema: "olbil",
                columns: table => new
                {
                    transfusionvitalsignsdetailid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    bloodtransfusionid = table.Column<int>(nullable: false),
                    transfusionphaseid = table.Column<int>(nullable: false),
                    arterialpressure = table.Column<decimal>(nullable: false),
                    temperaturec = table.Column<decimal>(nullable: false),
                    heartbeatratebpm = table.Column<decimal>(nullable: false),
                    respiratoryfrequence = table.Column<decimal>(nullable: false),
                    responsible = table.Column<string>(nullable: true),
                    observations = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transfusionvitalsignsdetail", x => x.transfusionvitalsignsdetailid);
                    table.ForeignKey(
                        name: "FK_transfusionvitalsignsdetail_bloodtransfusion_bloodtransfusi~",
                        column: x => x.bloodtransfusionid,
                        principalSchema: "olbil",
                        principalTable: "bloodtransfusion",
                        principalColumn: "bloodtransfusionid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transfusionvitalsignsdetail_transfusionphase_transfusionpha~",
                        column: x => x.transfusionphaseid,
                        principalSchema: "olbil",
                        principalTable: "transfusionphase",
                        principalColumn: "transfusionphaseid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bloodtransfusion_oncologypatientid",
                schema: "olbil",
                table: "bloodtransfusion",
                column: "oncologypatientid");

            migrationBuilder.CreateIndex(
                name: "IX_bloodtransfusion_wardid",
                schema: "olbil",
                table: "bloodtransfusion",
                column: "wardid");

            migrationBuilder.CreateIndex(
                name: "IX_transfusionproductdetail_bloodtransfusionid",
                schema: "olbil",
                table: "transfusionproductdetail",
                column: "bloodtransfusionid");

            migrationBuilder.CreateIndex(
                name: "IX_transfusionvitalsignsdetail_bloodtransfusionid",
                schema: "olbil",
                table: "transfusionvitalsignsdetail",
                column: "bloodtransfusionid");

            migrationBuilder.CreateIndex(
                name: "IX_transfusionvitalsignsdetail_transfusionphaseid",
                schema: "olbil",
                table: "transfusionvitalsignsdetail",
                column: "transfusionphaseid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transfusionproductdetail",
                schema: "olbil");

            migrationBuilder.DropTable(
                name: "transfusionvitalsignsdetail",
                schema: "olbil");

            migrationBuilder.DropTable(
                name: "bloodtransfusion",
                schema: "olbil");

            migrationBuilder.DropTable(
                name: "transfusionphase",
                schema: "olbil");
        }
    }
}

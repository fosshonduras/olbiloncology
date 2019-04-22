using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OLBIL.OncologyData.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "olbil");

            migrationBuilder.CreateTable(
                name: "administrativedivision",
                schema: "olbil",
                columns: table => new
                {
                    administrativedivisionid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    code = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_administrativedivision", x => x.administrativedivisionid);
                });

            migrationBuilder.CreateTable(
                name: "appointmentreason",
                schema: "olbil",
                columns: table => new
                {
                    appointmentreasonid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointmentreason", x => x.appointmentreasonid);
                });

            migrationBuilder.CreateTable(
                name: "appuser",
                schema: "olbil",
                columns: table => new
                {
                    appuserid = table.Column<Guid>(nullable: false),
                    username = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appuser", x => x.appuserid);
                });

            migrationBuilder.CreateTable(
                name: "building",
                schema: "olbil",
                columns: table => new
                {
                    buildingid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    code = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_building", x => x.buildingid);
                });

            migrationBuilder.CreateTable(
                name: "country",
                schema: "olbil",
                columns: table => new
                {
                    countryid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    isocode3 = table.Column<string>(nullable: true),
                    isocode2 = table.Column<string>(nullable: true),
                    nameen = table.Column<string>(nullable: true),
                    namees = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_country", x => x.countryid);
                });

            migrationBuilder.CreateTable(
                name: "diagnosis",
                schema: "olbil",
                columns: table => new
                {
                    diagnosisid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    icdcode = table.Column<string>(nullable: true),
                    completedescriptor = table.Column<string>(nullable: true),
                    shortdescriptor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diagnosis", x => x.diagnosisid);
                });

            migrationBuilder.CreateTable(
                name: "hospitalunit",
                schema: "olbil",
                columns: table => new
                {
                    hospitalunitid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    code = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hospitalunit", x => x.hospitalunitid);
                });

            migrationBuilder.CreateTable(
                name: "medicalspecialty",
                schema: "olbil",
                columns: table => new
                {
                    medicalspecialtyid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicalspecialty", x => x.medicalspecialtyid);
                });

            migrationBuilder.CreateTable(
                name: "person",
                schema: "olbil",
                columns: table => new
                {
                    personid = table.Column<Guid>(nullable: false),
                    firstname = table.Column<string>(nullable: true),
                    middlename = table.Column<string>(nullable: true),
                    lastname = table.Column<string>(nullable: true),
                    additionallastname = table.Column<string>(nullable: true),
                    preferredname = table.Column<string>(nullable: true),
                    temporaryidnumber = table.Column<string>(nullable: true),
                    governmentidnumber = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    addressline2 = table.Column<string>(nullable: true),
                    city = table.Column<string>(nullable: true),
                    state = table.Column<string>(nullable: true),
                    country = table.Column<string>(nullable: true),
                    homephone = table.Column<string>(nullable: true),
                    mobilephone = table.Column<string>(nullable: true),
                    nationality = table.Column<string>(nullable: true),
                    race = table.Column<string>(nullable: true),
                    gender = table.Column<string>(nullable: true),
                    birthdate = table.Column<DateTime>(nullable: true),
                    birthplace = table.Column<string>(nullable: true),
                    familystatus = table.Column<string>(nullable: true),
                    schoollevel = table.Column<string>(nullable: true),
                    methodoftranspotation = table.Column<string>(nullable: true),
                    appuserid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person", x => x.personid);
                    table.ForeignKey(
                        name: "FK_person_appuser_appuserid",
                        column: x => x.appuserid,
                        principalSchema: "olbil",
                        principalTable: "appuser",
                        principalColumn: "appuserid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ward",
                schema: "olbil",
                columns: table => new
                {
                    wardid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(nullable: true),
                    buildingid = table.Column<int>(nullable: false),
                    floornumber = table.Column<int>(nullable: false),
                    hospitalunitid = table.Column<int>(nullable: false),
                    wardgenderid = table.Column<int>(nullable: false),
                    wardstatusid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ward", x => x.wardid);
                    table.ForeignKey(
                        name: "FK_ward_building_buildingid",
                        column: x => x.buildingid,
                        principalSchema: "olbil",
                        principalTable: "building",
                        principalColumn: "buildingid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ward_hospitalunit_hospitalunitid",
                        column: x => x.hospitalunitid,
                        principalSchema: "olbil",
                        principalTable: "hospitalunit",
                        principalColumn: "hospitalunitid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "healthprofessional",
                schema: "olbil",
                columns: table => new
                {
                    healthprofessionalid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    personid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_healthprofessional", x => x.healthprofessionalid);
                    table.ForeignKey(
                        name: "FK_healthprofessional_person_personid",
                        column: x => x.personid,
                        principalSchema: "olbil",
                        principalTable: "person",
                        principalColumn: "personid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "oncologypatient",
                schema: "olbil",
                columns: table => new
                {
                    oncologypatientid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    registrationdate = table.Column<DateTime>(nullable: true),
                    admissiondate = table.Column<DateTime>(nullable: true),
                    informantsrelationship = table.Column<string>(nullable: true),
                    reasonforreferral = table.Column<string>(nullable: true),
                    personid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oncologypatient", x => x.oncologypatientid);
                    table.ForeignKey(
                        name: "FK_oncologypatient_person_personid",
                        column: x => x.personid,
                        principalSchema: "olbil",
                        principalTable: "person",
                        principalColumn: "personid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "bed",
                schema: "olbil",
                columns: table => new
                {
                    bedid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(nullable: true),
                    longdescription = table.Column<string>(nullable: true),
                    wardid = table.Column<int>(nullable: false),
                    bedstatusid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bed", x => x.bedid);
                    table.ForeignKey(
                        name: "FK_bed_ward_wardid",
                        column: x => x.wardid,
                        principalSchema: "olbil",
                        principalTable: "ward",
                        principalColumn: "wardid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "appointment",
                schema: "olbil",
                columns: table => new
                {
                    appointmentid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    oncologypatientid = table.Column<int>(nullable: false),
                    healthprofessionalid = table.Column<int>(nullable: true),
                    appointmentstatusid = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    attentionblocks = table.Column<string>(nullable: true),
                    patientattended = table.Column<bool>(nullable: false),
                    rescheduledappointmentid = table.Column<int>(nullable: true),
                    appointmentreasonid = table.Column<int>(nullable: true),
                    notes = table.Column<string>(nullable: true),
                    specialnotes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointment", x => x.appointmentid);
                    table.ForeignKey(
                        name: "FK_appointment_appointmentreason_appointmentreasonid",
                        column: x => x.appointmentreasonid,
                        principalSchema: "olbil",
                        principalTable: "appointmentreason",
                        principalColumn: "appointmentreasonid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_appointment_healthprofessional_healthprofessionalid",
                        column: x => x.healthprofessionalid,
                        principalSchema: "olbil",
                        principalTable: "healthprofessional",
                        principalColumn: "healthprofessionalid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_appointment_oncologypatient_oncologypatientid",
                        column: x => x.oncologypatientid,
                        principalSchema: "olbil",
                        principalTable: "oncologypatient",
                        principalColumn: "oncologypatientid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_appointment_appointment_rescheduledappointmentid",
                        column: x => x.rescheduledappointmentid,
                        principalSchema: "olbil",
                        principalTable: "appointment",
                        principalColumn: "appointmentid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "evolutioncard",
                schema: "olbil",
                columns: table => new
                {
                    evolutioncardid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    oncologypatientid = table.Column<int>(nullable: false),
                    appointmentid = table.Column<int>(nullable: true),
                    healthprofessionalid = table.Column<int>(nullable: true),
                    heightcm = table.Column<decimal>(nullable: true),
                    weightkg = table.Column<decimal>(nullable: true),
                    bodymassindex = table.Column<decimal>(nullable: true),
                    temperaturec = table.Column<decimal>(nullable: true),
                    heartbeatratebpm = table.Column<int>(nullable: true),
                    diagnosisid = table.Column<int>(nullable: true),
                    directions = table.Column<string>(nullable: true),
                    observations = table.Column<string>(nullable: true),
                    nextappointmentdate = table.Column<DateTime>(nullable: true),
                    referredto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evolutioncard", x => x.evolutioncardid);
                    table.ForeignKey(
                        name: "FK_evolutioncard_appointment_appointmentid",
                        column: x => x.appointmentid,
                        principalSchema: "olbil",
                        principalTable: "appointment",
                        principalColumn: "appointmentid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_evolutioncard_diagnosis_diagnosisid",
                        column: x => x.diagnosisid,
                        principalSchema: "olbil",
                        principalTable: "diagnosis",
                        principalColumn: "diagnosisid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_evolutioncard_healthprofessional_healthprofessionalid",
                        column: x => x.healthprofessionalid,
                        principalSchema: "olbil",
                        principalTable: "healthprofessional",
                        principalColumn: "healthprofessionalid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_evolutioncard_oncologypatient_oncologypatientid",
                        column: x => x.oncologypatientid,
                        principalSchema: "olbil",
                        principalTable: "oncologypatient",
                        principalColumn: "oncologypatientid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_appointment_appointmentreasonid",
                schema: "olbil",
                table: "appointment",
                column: "appointmentreasonid");

            migrationBuilder.CreateIndex(
                name: "IX_appointment_healthprofessionalid",
                schema: "olbil",
                table: "appointment",
                column: "healthprofessionalid");

            migrationBuilder.CreateIndex(
                name: "IX_appointment_oncologypatientid",
                schema: "olbil",
                table: "appointment",
                column: "oncologypatientid");

            migrationBuilder.CreateIndex(
                name: "IX_appointment_rescheduledappointmentid",
                schema: "olbil",
                table: "appointment",
                column: "rescheduledappointmentid");

            migrationBuilder.CreateIndex(
                name: "IX_bed_wardid",
                schema: "olbil",
                table: "bed",
                column: "wardid");

            migrationBuilder.CreateIndex(
                name: "IX_evolutioncard_appointmentid",
                schema: "olbil",
                table: "evolutioncard",
                column: "appointmentid");

            migrationBuilder.CreateIndex(
                name: "IX_evolutioncard_diagnosisid",
                schema: "olbil",
                table: "evolutioncard",
                column: "diagnosisid");

            migrationBuilder.CreateIndex(
                name: "IX_evolutioncard_healthprofessionalid",
                schema: "olbil",
                table: "evolutioncard",
                column: "healthprofessionalid");

            migrationBuilder.CreateIndex(
                name: "IX_evolutioncard_oncologypatientid",
                schema: "olbil",
                table: "evolutioncard",
                column: "oncologypatientid");

            migrationBuilder.CreateIndex(
                name: "IX_healthprofessional_personid",
                schema: "olbil",
                table: "healthprofessional",
                column: "personid");

            migrationBuilder.CreateIndex(
                name: "IX_oncologypatient_personid",
                schema: "olbil",
                table: "oncologypatient",
                column: "personid");

            migrationBuilder.CreateIndex(
                name: "IX_person_appuserid",
                schema: "olbil",
                table: "person",
                column: "appuserid");

            migrationBuilder.CreateIndex(
                name: "IX_ward_buildingid",
                schema: "olbil",
                table: "ward",
                column: "buildingid");

            migrationBuilder.CreateIndex(
                name: "IX_ward_hospitalunitid",
                schema: "olbil",
                table: "ward",
                column: "hospitalunitid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "administrativedivision",
                schema: "olbil");

            migrationBuilder.DropTable(
                name: "bed",
                schema: "olbil");

            migrationBuilder.DropTable(
                name: "country",
                schema: "olbil");

            migrationBuilder.DropTable(
                name: "evolutioncard",
                schema: "olbil");

            migrationBuilder.DropTable(
                name: "medicalspecialty",
                schema: "olbil");

            migrationBuilder.DropTable(
                name: "ward",
                schema: "olbil");

            migrationBuilder.DropTable(
                name: "appointment",
                schema: "olbil");

            migrationBuilder.DropTable(
                name: "diagnosis",
                schema: "olbil");

            migrationBuilder.DropTable(
                name: "building",
                schema: "olbil");

            migrationBuilder.DropTable(
                name: "hospitalunit",
                schema: "olbil");

            migrationBuilder.DropTable(
                name: "appointmentreason",
                schema: "olbil");

            migrationBuilder.DropTable(
                name: "healthprofessional",
                schema: "olbil");

            migrationBuilder.DropTable(
                name: "oncologypatient",
                schema: "olbil");

            migrationBuilder.DropTable(
                name: "person",
                schema: "olbil");

            migrationBuilder.DropTable(
                name: "appuser",
                schema: "olbil");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OLBIL.OncologyWebApp.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OncologyPatients",
                columns: table => new
                {
                    OncologyPatientId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    AdditionalLastName = table.Column<string>(nullable: true),
                    PreferredName = table.Column<string>(nullable: true),
                    GovernmentIDNumber = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    HomePhone = table.Column<string>(nullable: true),
                    MobilePhone = table.Column<string>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: true),
                    AdmissionDate = table.Column<DateTime>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    Race = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Birthdate = table.Column<DateTime>(nullable: true),
                    Birthplace = table.Column<string>(nullable: true),
                    FamilyStatus = table.Column<string>(nullable: true),
                    SchoolLevel = table.Column<string>(nullable: true),
                    MethodOfTranspotation = table.Column<string>(nullable: true),
                    InformantsRelationship = table.Column<string>(nullable: true),
                    ReasonForReferral = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OncologyPatients", x => x.OncologyPatientId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OncologyPatients");
        }
    }
}

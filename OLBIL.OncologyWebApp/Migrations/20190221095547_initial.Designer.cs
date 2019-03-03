﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OLBIL.OncologyData;

namespace OLBIL.OncologyWebApp.Migrations
{
    [DbContext(typeof(OncologyContext))]
    [Migration("20190221095547_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("OLBIL.OncologyWebApp.Entities.OncologyPatient", b =>
                {
                    b.Property<int>("OncologyPatientId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdditionalLastName");

                    b.Property<string>("Address");

                    b.Property<string>("AddressLine2");

                    b.Property<DateTime?>("AdmissionDate");

                    b.Property<DateTime?>("Birthdate");

                    b.Property<string>("Birthplace");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("FamilyStatus");

                    b.Property<string>("FirstName");

                    b.Property<string>("Gender");

                    b.Property<string>("GovernmentIDNumber");

                    b.Property<string>("HomePhone");

                    b.Property<string>("InformantsRelationship");

                    b.Property<string>("LastName");

                    b.Property<string>("MethodOfTranspotation");

                    b.Property<string>("MiddleName");

                    b.Property<string>("MobilePhone");

                    b.Property<string>("Nationality");

                    b.Property<string>("PreferredName");

                    b.Property<string>("Race");

                    b.Property<string>("ReasonForReferral");

                    b.Property<DateTime?>("RegistrationDate");

                    b.Property<string>("SchoolLevel");

                    b.Property<string>("State");

                    b.HasKey("OncologyPatientId");

                    b.ToTable("OncologyPatients");
                });
#pragma warning restore 612, 618
        }
    }
}

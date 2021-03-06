﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OLBIL.OncologyData;

namespace OLBIL.OncologyData.Migrations
{
    [DbContext(typeof(OncologyContext))]
    [Migration("20190421225508_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("olbil")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("OLBIL.OncologyDomain.Entities.AdministrativeDivision", b =>
                {
                    b.Property<int>("AdministrativeDivisionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("administrativedivisionid");

                    b.Property<string>("Code")
                        .HasColumnName("code");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.HasKey("AdministrativeDivisionId");

                    b.ToTable("administrativedivision");
                });

            modelBuilder.Entity("OLBIL.OncologyDomain.Entities.AppUser", b =>
                {
                    b.Property<Guid>("AppUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("appuserid");

                    b.Property<string>("Password")
                        .HasColumnName("password");

                    b.Property<string>("Username")
                        .HasColumnName("username");

                    b.HasKey("AppUserId");

                    b.ToTable("appuser");
                });

            modelBuilder.Entity("OLBIL.OncologyDomain.Entities.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("appointmentid");

                    b.Property<int?>("AppointmentReasonId")
                        .HasColumnName("appointmentreasonid");

                    b.Property<int>("AppointmentStatusId")
                        .HasColumnName("appointmentstatusid");

                    b.Property<string>("AttentionBlocks")
                        .HasColumnName("attentionblocks");

                    b.Property<DateTime>("Date")
                        .HasColumnName("date");

                    b.Property<int?>("HealthProfessionalId")
                        .HasColumnName("healthprofessionalid");

                    b.Property<string>("Notes")
                        .HasColumnName("notes");

                    b.Property<int>("OncologyPatientId")
                        .HasColumnName("oncologypatientid");

                    b.Property<bool>("PatientAttended")
                        .HasColumnName("patientattended");

                    b.Property<int?>("RescheduledAppointmentId")
                        .HasColumnName("rescheduledappointmentid");

                    b.Property<string>("SpecialNotes")
                        .HasColumnName("specialnotes");

                    b.HasKey("AppointmentId");

                    b.HasIndex("AppointmentReasonId");

                    b.HasIndex("HealthProfessionalId");

                    b.HasIndex("OncologyPatientId");

                    b.HasIndex("RescheduledAppointmentId");

                    b.ToTable("appointment");
                });

            modelBuilder.Entity("OLBIL.OncologyDomain.Entities.AppointmentReason", b =>
                {
                    b.Property<int>("AppointmentReasonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("appointmentreasonid");

                    b.Property<string>("Description")
                        .HasColumnName("description");

                    b.HasKey("AppointmentReasonId");

                    b.ToTable("appointmentreason");
                });

            modelBuilder.Entity("OLBIL.OncologyDomain.Entities.Bed", b =>
                {
                    b.Property<int>("BedId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("bedid");

                    b.Property<int>("BedStatusId")
                        .HasColumnName("bedstatusid");

                    b.Property<string>("LongDescription")
                        .HasColumnName("longdescription");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.Property<int>("WardId")
                        .HasColumnName("wardid");

                    b.HasKey("BedId");

                    b.HasIndex("WardId");

                    b.ToTable("bed");
                });

            modelBuilder.Entity("OLBIL.OncologyDomain.Entities.Building", b =>
                {
                    b.Property<int>("BuildingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("buildingid");

                    b.Property<string>("Code")
                        .HasColumnName("code");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.HasKey("BuildingId");

                    b.ToTable("building");
                });

            modelBuilder.Entity("OLBIL.OncologyDomain.Entities.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("countryid");

                    b.Property<string>("ISOCode2")
                        .HasColumnName("isocode2");

                    b.Property<string>("ISOCode3")
                        .HasColumnName("isocode3");

                    b.Property<string>("NameEn")
                        .HasColumnName("nameen");

                    b.Property<string>("NameEs")
                        .HasColumnName("namees");

                    b.HasKey("CountryId");

                    b.ToTable("country");
                });

            modelBuilder.Entity("OLBIL.OncologyDomain.Entities.Diagnosis", b =>
                {
                    b.Property<int>("DiagnosisId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("diagnosisid");

                    b.Property<string>("CompleteDescriptor")
                        .HasColumnName("completedescriptor");

                    b.Property<string>("ICDCode")
                        .HasColumnName("icdcode");

                    b.Property<string>("ShortDescriptor")
                        .HasColumnName("shortdescriptor");

                    b.HasKey("DiagnosisId");

                    b.ToTable("diagnosis");
                });

            modelBuilder.Entity("OLBIL.OncologyDomain.Entities.EvolutionCard", b =>
                {
                    b.Property<int>("EvolutionCardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("evolutioncardid");

                    b.Property<int?>("AppointmentId")
                        .HasColumnName("appointmentid");

                    b.Property<decimal?>("BodyMassIndex")
                        .HasColumnName("bodymassindex");

                    b.Property<int?>("DiagnosisId")
                        .HasColumnName("diagnosisid");

                    b.Property<string>("Directions")
                        .HasColumnName("directions");

                    b.Property<int?>("HealthProfessionalId")
                        .HasColumnName("healthprofessionalid");

                    b.Property<int?>("HeartBeatRateBpm")
                        .HasColumnName("heartbeatratebpm");

                    b.Property<decimal?>("HeightCm")
                        .HasColumnName("heightcm");

                    b.Property<DateTime?>("NextAppointmentDate")
                        .HasColumnName("nextappointmentdate");

                    b.Property<string>("Observations")
                        .HasColumnName("observations");

                    b.Property<int>("OncologyPatientId")
                        .HasColumnName("oncologypatientid");

                    b.Property<string>("ReferredTo")
                        .HasColumnName("referredto");

                    b.Property<decimal?>("TemperatureC")
                        .HasColumnName("temperaturec");

                    b.Property<decimal?>("WeightKg")
                        .HasColumnName("weightkg");

                    b.HasKey("EvolutionCardId");

                    b.HasIndex("AppointmentId");

                    b.HasIndex("DiagnosisId");

                    b.HasIndex("HealthProfessionalId");

                    b.HasIndex("OncologyPatientId");

                    b.ToTable("evolutioncard");
                });

            modelBuilder.Entity("OLBIL.OncologyDomain.Entities.HealthProfessional", b =>
                {
                    b.Property<int>("HealthProfessionalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("healthprofessionalid");

                    b.Property<Guid?>("PersonId")
                        .HasColumnName("personid");

                    b.HasKey("HealthProfessionalId");

                    b.HasIndex("PersonId");

                    b.ToTable("healthprofessional");
                });

            modelBuilder.Entity("OLBIL.OncologyDomain.Entities.HospitalUnit", b =>
                {
                    b.Property<int>("HospitalUnitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("hospitalunitid");

                    b.Property<string>("Code")
                        .HasColumnName("code");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.HasKey("HospitalUnitId");

                    b.ToTable("hospitalunit");
                });

            modelBuilder.Entity("OLBIL.OncologyDomain.Entities.MedicalSpecialty", b =>
                {
                    b.Property<int>("MedicalSpecialtyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("medicalspecialtyid");

                    b.Property<string>("Description")
                        .HasColumnName("description");

                    b.HasKey("MedicalSpecialtyId");

                    b.ToTable("medicalspecialty");
                });

            modelBuilder.Entity("OLBIL.OncologyDomain.Entities.OncologyPatient", b =>
                {
                    b.Property<int>("OncologyPatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("oncologypatientid");

                    b.Property<DateTime?>("AdmissionDate")
                        .HasColumnName("admissiondate");

                    b.Property<string>("InformantsRelationship")
                        .HasColumnName("informantsrelationship");

                    b.Property<Guid?>("PersonId")
                        .HasColumnName("personid");

                    b.Property<string>("ReasonForReferral")
                        .HasColumnName("reasonforreferral");

                    b.Property<DateTime?>("RegistrationDate")
                        .HasColumnName("registrationdate");

                    b.HasKey("OncologyPatientId");

                    b.HasIndex("PersonId");

                    b.ToTable("oncologypatient");
                });

            modelBuilder.Entity("OLBIL.OncologyDomain.Entities.Person", b =>
                {
                    b.Property<Guid>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("personid");

                    b.Property<string>("AdditionalLastName")
                        .HasColumnName("additionallastname");

                    b.Property<string>("Address")
                        .HasColumnName("address");

                    b.Property<string>("AddressLine2")
                        .HasColumnName("addressline2");

                    b.Property<Guid?>("AppUserId")
                        .HasColumnName("appuserid");

                    b.Property<DateTime?>("Birthdate")
                        .HasColumnName("birthdate");

                    b.Property<string>("Birthplace")
                        .HasColumnName("birthplace");

                    b.Property<string>("City")
                        .HasColumnName("city");

                    b.Property<string>("Country")
                        .HasColumnName("country");

                    b.Property<string>("FamilyStatus")
                        .HasColumnName("familystatus");

                    b.Property<string>("FirstName")
                        .HasColumnName("firstname");

                    b.Property<string>("Gender")
                        .HasColumnName("gender");

                    b.Property<string>("GovernmentIDNumber")
                        .HasColumnName("governmentidnumber");

                    b.Property<string>("HomePhone")
                        .HasColumnName("homephone");

                    b.Property<string>("LastName")
                        .HasColumnName("lastname");

                    b.Property<string>("MethodOfTranspotation")
                        .HasColumnName("methodoftranspotation");

                    b.Property<string>("MiddleName")
                        .HasColumnName("middlename");

                    b.Property<string>("MobilePhone")
                        .HasColumnName("mobilephone");

                    b.Property<string>("Nationality")
                        .HasColumnName("nationality");

                    b.Property<string>("PreferredName")
                        .HasColumnName("preferredname");

                    b.Property<string>("Race")
                        .HasColumnName("race");

                    b.Property<string>("SchoolLevel")
                        .HasColumnName("schoollevel");

                    b.Property<string>("State")
                        .HasColumnName("state");

                    b.Property<string>("TemporaryIdNumber")
                        .HasColumnName("temporaryidnumber");

                    b.HasKey("PersonId");

                    b.HasIndex("AppUserId");

                    b.ToTable("person","olbil");
                });

            modelBuilder.Entity("OLBIL.OncologyDomain.Entities.Ward", b =>
                {
                    b.Property<int>("WardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("wardid");

                    b.Property<int>("BuildingId")
                        .HasColumnName("buildingid");

                    b.Property<int>("FloorNumber")
                        .HasColumnName("floornumber");

                    b.Property<int>("HospitalUnitId")
                        .HasColumnName("hospitalunitid");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.Property<int>("WardGenderId")
                        .HasColumnName("wardgenderid");

                    b.Property<int>("WardStatusId")
                        .HasColumnName("wardstatusid");

                    b.HasKey("WardId");

                    b.HasIndex("BuildingId");

                    b.HasIndex("HospitalUnitId");

                    b.ToTable("ward");
                });

            modelBuilder.Entity("OLBIL.OncologyDomain.Entities.Appointment", b =>
                {
                    b.HasOne("OLBIL.OncologyDomain.Entities.AppointmentReason", "AppointmentReason")
                        .WithMany()
                        .HasForeignKey("AppointmentReasonId");

                    b.HasOne("OLBIL.OncologyDomain.Entities.HealthProfessional", "HealthProfessional")
                        .WithMany()
                        .HasForeignKey("HealthProfessionalId");

                    b.HasOne("OLBIL.OncologyDomain.Entities.OncologyPatient", "OncologyPatient")
                        .WithMany("Appointments")
                        .HasForeignKey("OncologyPatientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OLBIL.OncologyDomain.Entities.Appointment", "RescheduledAppointment")
                        .WithMany()
                        .HasForeignKey("RescheduledAppointmentId");
                });

            modelBuilder.Entity("OLBIL.OncologyDomain.Entities.Bed", b =>
                {
                    b.HasOne("OLBIL.OncologyDomain.Entities.Ward", "Ward")
                        .WithMany("Beds")
                        .HasForeignKey("WardId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OLBIL.OncologyDomain.Entities.EvolutionCard", b =>
                {
                    b.HasOne("OLBIL.OncologyDomain.Entities.Appointment", "Appointment")
                        .WithMany()
                        .HasForeignKey("AppointmentId");

                    b.HasOne("OLBIL.OncologyDomain.Entities.Diagnosis", "Diagnosis")
                        .WithMany()
                        .HasForeignKey("DiagnosisId");

                    b.HasOne("OLBIL.OncologyDomain.Entities.HealthProfessional", "HealthProfessional")
                        .WithMany()
                        .HasForeignKey("HealthProfessionalId");

                    b.HasOne("OLBIL.OncologyDomain.Entities.OncologyPatient", "OncologyPatient")
                        .WithMany("EvolutionCards")
                        .HasForeignKey("OncologyPatientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OLBIL.OncologyDomain.Entities.HealthProfessional", b =>
                {
                    b.HasOne("OLBIL.OncologyDomain.Entities.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");
                });

            modelBuilder.Entity("OLBIL.OncologyDomain.Entities.OncologyPatient", b =>
                {
                    b.HasOne("OLBIL.OncologyDomain.Entities.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");
                });

            modelBuilder.Entity("OLBIL.OncologyDomain.Entities.Person", b =>
                {
                    b.HasOne("OLBIL.OncologyDomain.Entities.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId");
                });

            modelBuilder.Entity("OLBIL.OncologyDomain.Entities.Ward", b =>
                {
                    b.HasOne("OLBIL.OncologyDomain.Entities.Building", "Building")
                        .WithMany("Wards")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OLBIL.OncologyDomain.Entities.HospitalUnit", "Unit")
                        .WithMany("Wards")
                        .HasForeignKey("HospitalUnitId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

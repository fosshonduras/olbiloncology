-- User: appclientuser
-- DROP USER appclientuser;

CREATE USER appclientuser WITH
  LOGIN
  NOSUPERUSER
  INHERIT
  CREATEDB
  NOCREATEROLE
  NOREPLICATION;

ALTER DEFAULT PRIVILEGES
GRANT SELECT, INSERT
ON TABLES
TO PUBLIC;

ALTER DEFAULT PRIVILEGES
GRANT ALL
ON TABLES
TO appclientuser;

ALTER DEFAULT PRIVILEGES
GRANT SELECT
ON SEQUENCES
TO PUBLIC;

ALTER DEFAULT PRIVILEGES
GRANT ALL
ON SEQUENCES
TO appclientuser;

ALTER DEFAULT PRIVILEGES
GRANT EXECUTE
ON FUNCTIONS
TO PUBLIC;

ALTER DEFAULT PRIVILEGES
GRANT ALL
ON FUNCTIONS
TO appclientuser;

ALTER DEFAULT PRIVILEGES
GRANT USAGE
ON TYPES
TO PUBLIC;

ALTER DEFAULT PRIVILEGES
GRANT ALL
ON TYPES
TO PUBLIC;

CREATE SCHEMA olbil;
GRANT ALL ON SCHEMA olbil TO appclientuser;


CREATE TABLE olbil.PersonGender(
    PersonGenderId SERIAL NOT NULL,
    Name VARCHAR(50) COLLATE pg_catalog.default  NOT NULL UNIQUE,
    CONSTRAINT PersonGender_pkey PRIMARY KEY (PesonGenderId)
)WITH(
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE olbil.Race(
    RaceId SERIAL NOT NULL,
    Name VARCHAR(50) COLLATE pg_catalog.default  NOT NULL UNIQUE,
    CONSTRAINT Race_pkey PRIMARY KEY (RaceId)
)WITH(
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE olbil.Building(
    BuildingId SERIAL NOT NULL,
    Code VARCHAR(50) COLLATE pg_catalog.default NOT NULL UNIQUE,
    Name VARCHAR(256) COLLATE pg_catalog.default NOT NULL UNIQUE,
    CONSTRAINT Building_pkey PRIMARY KEY (BuildingId)
)
WITH(
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE olbil.HospitalUnit(
    HospitalUnitId SERIAL NOT NULL,
    Code VARCHAR(50) COLLATE pg_catalog.default NOT NULL,
    Name VARCHAR(256) COLLATE pg_catalog.default NOT NULL,
    CONSTRAINT HospitalUnit_pkey PRIMARY KEY (HospitalUnitId)
)WITH(
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE olbil.Ward(
    WardId SERIAL NOT NULL,
    Name VARCHAR(256) COLLATE pg_catalog.default NOT NULL,
    BuildingId INT NOT NULL,
    FloorNumber INT NOT NULL,
    HospitalUnitId INT NOT NULL,
    WardGenderId INT NOT NULL,
    WardStatusId INT NOT NULL,
    CONSTRAINT Ward_pkey PRIMARY KEY (WardId),
    CONSTRAINT Ward_Building_buildingid_fkey FOREIGN KEY (BuildingId)
        REFERENCES olbil.Building(BuildingId)
        ON UPDATE RESTRICT ON DELETE RESTRICT,
    CONSTRAINT Ward_HospitalUnit_unitid_fkey FOREIGN KEY (HospitalUnitId)
        REFERENCES olbil.HospitalUnit(HospitalUnitId)
        ON UPDATE RESTRICT ON DELETE RESTRICT,
    CONSTRAINT Ward_WardGender_genderid_fkey FOREIGN KEY (WardGenderId)
        REFERENCES olbil.WardGender(WardGenderId)
        ON UPDATE RESTRICT ON DELETE RESTRICT,
    CONSTRAINT Ward_WardStatus_wardstatusid_fkey FOREIGN KEY (WardStatusId)
        REFERENCES olbil.WardStatus(WardStatusId)
        ON UPDATE RESTRICT ON DELETE RESTRICT
)WITH(
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE olbil.ward
    ADD COLUMN hospitalunitid integer NOT NULL;

ALTER TABLE olbil.Ward
ADD CONSTRAINT Ward_HospitalUnit_unitid_fkey
FOREIGN KEY (HospitalUnitId)
REFERENCES olbil.HospitalUnit (HospitalUnitId)
ON UPDATE RESTRICT ON DELETE RESTRICT

CREATE TABLE olbil.Bed
(
    BedId SERIAL NOT NULL,
    Name VARCHAR(256) COLLATE pg_catalog.default NOT NULL,
    LongDescription VARCHAR(256) COLLATE pg_catalog.default NOT NULL,
    BedStatusId INT NOT NULL,
    WardId INT NOT NULL,
    CONSTRAINT Bed_pkey PRIMARY KEY (BedId),
    CONSTRAINT Bed_Ward_wardid_fkey FOREIGN KEY (WardId)
        REFERENCES olbil.Ward(WardId)
        ON UPDATE RESTRICT ON DELETE RESTRICT
)
WITH(
    OIDS = FALSE
)
TABLESPACE pg_default;


CREATE TABLE olbil.AppUser
(
    AppUserId UUID NOT NULL,
    Username VARCHAR(50) COLLATE pg_catalog.default NOT NULL,
    Password VARCHAR(512) COLLATE pg_catalog.default NOT NULL,
    CONSTRAINT PK_AppUsers PRIMARY KEY (AppUserId)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE olbil.Person
(
    PersonId UUID NOT NULL,
    FirstName VARCHAR(50) COLLATE pg_catalog.default,
    MiddleName VARCHAR(50) COLLATE pg_catalog.default,
    LastName VARCHAR(50) COLLATE pg_catalog.default,
    AdditionalLastName VARCHAR(50) COLLATE pg_catalog.default,
    PreferredName VARCHAR(50) COLLATE pg_catalog.default,
    GovernmentIDNumber VARCHAR(100) COLLATE pg_catalog.default,
    Address TEXT COLLATE pg_catalog.default,
    AddressLine2 TEXT COLLATE pg_catalog.default,
    City VARCHAR(100) COLLATE pg_catalog.default,
    State VARCHAR(100) COLLATE pg_catalog.default,
    Country VARCHAR(100) COLLATE pg_catalog.default,
    HomePhone VARCHAR(100) COLLATE pg_catalog.default,
    MobilePhone VARCHAR(100) COLLATE pg_catalog.default,
    Nationality VARCHAR(100) COLLATE pg_catalog.default,
    Race VARCHAR(100) COLLATE pg_catalog.default,
    Gender VARCHAR(100) COLLATE pg_catalog.default,
    Birthdate TIMESTAMP WITHOUT TIME ZONE,
    Birthplace TEXT COLLATE pg_catalog.default,
    FamilyStatus VARCHAR(30) COLLATE pg_catalog.default,
    SchoolLevel VARCHAR(30) COLLATE pg_catalog.default,
    MethodOfTranspotation VARCHAR(30) COLLATE pg_catalog.default,
    AppUserId UUID,
    CONSTRAINT PK_Person PRIMARY KEY (PersonId),
    CONSTRAINT FK_Person_AppUser_AppUserId FOREIGN KEY (AppUserId)
        REFERENCES olbil.AppUser (AppUserId) MATCH SIMPLE
        ON UPDATE RESTRICT ON DELETE RESTRICT
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;


CREATE TABLE olbil.OncologyPatient
(
    OncologyPatientId SERIAL NOT NULL,
    RegistrationDate TIMESTAMP WITHOUT TIME ZONE,
    AdmissionDate TIMESTAMP WITHOUT TIME ZONE,
    InformantsRelationship VARCHAR(50) COLLATE pg_catalog.default,
    ReasonForReferral VARCHAR(50) COLLATE pg_catalog.default,
    PersonId UUID,
    CONSTRAINT PK_OncologyPatients PRIMARY KEY (OncologyPatientId),
    CONSTRAINT FK_OncologyPatients_Person_PersonId FOREIGN KEY (PersonId)
        REFERENCES olbil.Person (PersonId) MATCH SIMPLE
        ON UPDATE RESTRICT ON DELETE RESTRICT
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;


CREATE TABLE olbil.HealthProfessional
(
    HealthProfessionalId SERIAL NOT NULL,
    RegistrationDate TIMESTAMP WITHOUT TIME ZONE,
    PersonId UUID,
    CONSTRAINT PK_HealthProfessional PRIMARY KEY (HealthProfessionalId),
    CONSTRAINT HealthProfessional_Person_PersonId FOREIGN KEY (PersonId)
        REFERENCES olbil.Person (PersonId) MATCH SIMPLE
        ON UPDATE RESTRICT ON DELETE RESTRICT
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE olbil.AdministrativeDivision
(
    AdministrativeDivisionId SERIAL NOT NULL,
    Code VARCHAR(20) COLLATE pg_catalog.default NOT NULL,
    Name VARCHAR(50) COLLATE pg_catalog.default NOT NULL,
    Level INT NOT NULL,
    ParentId INT,
    CONSTRAINT PK_AdministrativeDivision PRIMARY KEY (AdministrativeDivisionId),
    CONSTRAINT UK_AdministrativeDivision_Code UNIQUE (Code),
    CONSTRAINT AdministrativeDivision_AdministrativeDivision_ParentId FOREIGN KEY (ParentId)
        REFERENCES olbil.AdministrativeDivision (AdministrativeDivisionId) MATCH SIMPLE
        ON UPDATE RESTRICT ON DELETE RESTRICT
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE olbil.Country(
    CountryId SERIAL NOT NULL,
    NameEn VARCHAR(50) COLLATE pg_catalog.default NOT NULL,
    NameEs VARCHAR(50) COLLATE pg_catalog.default NOT NULL,
    ISOCode3 VARCHAR(3) COLLATE pg_catalog.default NOT NULL,
    ISOCode2 VARCHAR(3) COLLATE pg_catalog.default NOT NULL,
    CONSTRAINT PK_County PRIMARY KEY (CountryId),
    CONSTRAINT UK_Country_ISOCode3 UNIQUE (ISOCode3),
    CONSTRAINT UK_Country_ISOCode2 UNIQUE (ISOCode2)
)WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;


CREATE TABLE olbil.Diagnosis(
    DiagnosisId SERIAL NOT NULL,
    ICDCode VARCHAR(20) COLLATE pg_catalog.default NOT NULL,
    CompleteDescriptor VARCHAR(512) COLLATE pg_catalog.default NOT NULL,
    ShortDescriptor VARCHAR(512) COLLATE pg_catalog.default NOT NULL,
    CONSTRAINT PK_Diagnosis PRIMARY KEY (DiagnosisId),
    CONSTRAINT UK_Diagnosis_ICDCode UNIQUE (ICDCode)
)WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE olbil.Appointment(
    AppointmentId SERIAL NOT NULL,
    Date TIMESTAMP NOT NULL,
    AppointmentReasonId INT,
    OncologyPatientId INT NOT NULL,
    HealthProfessionalId INT,
    PattientAttended BIT,
    AttentionBlocks VARCHAR(256) COLLATE pg_catalog.default,
    Notes VARCHAR(4092) COLLATE pg_catalog.default,
    SpecialNotes VARCHAR(4092) COLLATE pg_catalog.default,
    RescheduledAppointmentId INT,
    CONSTRAINT PK_Appointment PRIMARY KEY (AppointmentId),
    CONSTRAINT Appointment_AppointmentReason_AppointmentReasonId FOREIGN KEY (AppointmentReasonId)
        REFERENCES olbil.AppointmentReason (AppointmentReasonId) MATCH SIMPLE
        ON UPDATE RESTRICT ON DELETE RESTRICT,
    CONSTRAINT Appointment_Appointment_RescheduledAppointmentId FOREIGN KEY (RescheduledAppointmentId)
        REFERENCES olbil.Appointment (AppointmentId) MATCH SIMPLE
        ON UPDATE RESTRICT ON DELETE RESTRICT,
    CONSTRAINT Appointment_OncologyPatient_OncologyPatientId FOREIGN KEY (OncologyPatientId)
        REFERENCES olbil.OncologyPatient (OncologyPatientId) MATCH SIMPLE
        ON UPDATE RESTRICT ON DELETE RESTRICT,
    CONSTRAINT Appointment_HealthProfessional_HealthProfessionalId FOREIGN KEY (HealthProfessionalId)
        REFERENCES olbil.HealthProfessional(HealthProfessionalId) MATCH SIMPLE
        ON UPDATE RESTRICT ON DELETE RESTRICT,
    CONSTRAINT UK_Appointment_PatientDateReason UNIQUE (OncologyPatientId, Date, AppointmentReasonId)
)WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;


CREATE TABLE olbil.EvolutionCard(
    EvolutionCardId SERIAL NOT NULL,
	OncologyPatientId INT NOT NULL,
	HealthProfessionalId INT,
	AppointmentId INT,
    DiagnosisId INT,
	HeightCm DECIMAL(5,2),
	WeightKg DECIMAL(6,2),
	TemperatureC DECIMAL(5,2),
	HeartBeatRateBpm INT,
    Observations VARCHAR(4092) COLLATE pg_catalog.default,
	Directions VARCHAR(4092) COLLATE pg_catalog.default,
	NextAppointmentDate TIMESTAMP,
	ReferredTo VARCHAR(256) COLLATE pg_catalog.default,

    CONSTRAINT PK_EvolutionCard PRIMARY KEY (EvolutionCardId),
	CONSTRAINT EvolutionCard_OncologyPatient_OncologyPatientId FOREIGN KEY (OncologyPatientId)
        REFERENCES olbil.OncologyPatient (OncologyPatientId) MATCH SIMPLE
        ON UPDATE RESTRICT ON DELETE RESTRICT,
	CONSTRAINT EvolutionCard_HealthProfessional_HealthProfessionalId FOREIGN KEY (HealthProfessionalId)
        REFERENCES olbil.HealthProfessional(HealthProfessionalId) MATCH SIMPLE
        ON UPDATE RESTRICT ON DELETE RESTRICT,
    CONSTRAINT EvolutionCard_Diagnosis_ADiagnosisId FOREIGN KEY (DiagnosisId)
        REFERENCES olbil.Diagnosis (DiagnosisId) MATCH SIMPLE
        ON UPDATE RESTRICT ON DELETE RESTRICT,
    CONSTRAINT EvolutionCard_Appointment_AppointmentId FOREIGN KEY (AppointmentId)
        REFERENCES olbil.Appointment (AppointmentId) MATCH SIMPLE
        ON UPDATE RESTRICT ON DELETE RESTRICT,
    CONSTRAINT UK_EvolutionCard_OncologyPatientAppointment UNIQUE (OncologyPatientId, AppointmentId)
)WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER DEFAULT PRIVILEGES
GRANT ALL
ON TABLES
TO PUBLIC

GRANT ALL
ON SEQUENCES
TO PUBLIC

GRANT ALL
ON FUNCTIONS
TO PUBLIC

GRANT ALL
ON TYPES
TO PUBLIC;

CREATE SCHEMA olbil;
GRANT ALL ON SCHEMA olbil TO PUBLIC;

CREATE TABLE olbil.BedStatus(
    BedStatusId SERIAL NOT NULL,
    Name VARCHAR(50) COLLATE pg_catalog.default  NOT NULL UNIQUE,
    CONSTRAINT BedStatus_pkey PRIMARY KEY (BedStatusId)
)WITH(
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE olbil.WardStatus(
    WardStatusId SERIAL NOT NULL,
    Name VARCHAR(50) COLLATE pg_catalog.default  NOT NULL UNIQUE,
    CONSTRAINT WardStatus_pkey PRIMARY KEY (WardStatusId)
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

CREATE TABLE olbil.Unit(
    UnitId SERIAL NOT NULL,
    Code VARCHAR(50) COLLATE pg_catalog.default NOT NULL,
    Name VARCHAR(256) COLLATE pg_catalog.default NOT NULL,
    CONSTRAINT Unit_pkey PRIMARY KEY (UnitId)
)WITH(
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE olbil.WardGender(
    WardGenderId SERIAL NOT NULL,
    Name VARCHAR(50) COLLATE pg_catalog.default  NOT NULL UNIQUE,
    CONSTRAINT WardGender_pkey PRIMARY KEY (WardGenderId)
)WITH(
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE olbil.Ward(
    WardId SERIAL NOT NULL,
    Name VARCHAR(256) COLLATE pg_catalog.default NOT NULL,
    BuildingId INT NOT NULL,
    FloorNumber INT NOT NULL,
    UnitId INT NOT NULL,
    WardGenderId INT NOT NULL,
    WardStatusId INT NOT NULL,
    CONSTRAINT Ward_pkey PRIMARY KEY (WardId),
    CONSTRAINT Ward_Building_buildingid_fkey FOREIGN KEY (BuildingId)
        REFERENCES olbil.Building(BuildingId)
        ON UPDATE RESTRICT ON DELETE RESTRICT,
    CONSTRAINT Ward_Unit_unitid_fkey FOREIGN KEY (UnitId)
        REFERENCES olbil.Unit(UnitId)
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


CREATE TABLE olbil.Bed
(
    BedId INT NOT NULL,
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

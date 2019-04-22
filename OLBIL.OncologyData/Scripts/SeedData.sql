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

CREATE TABLE olbil.WardGender(
    WardGenderId SERIAL NOT NULL,
    Name VARCHAR(50) COLLATE pg_catalog.default  NOT NULL UNIQUE,
    CONSTRAINT WardGender_pkey PRIMARY KEY (WardGenderId)
)WITH(
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE olbil.BloodType(
    BloodTypeId SERIAL NOT NULL,
    NameEn VARCHAR(20) COLLATE pg_catalog.default NOT NULL,
    NameEs VARCHAR(20) COLLATE pg_catalog.default NOT NULL,
    Display VARCHAR(10) COLLATE pg_catalog.default NOT NULL,
    CONSTRAINT PK_BloodType PRIMARY KEY (BloodTypeId),
    CONSTRAINT UK_BloodType_NameEn UNIQUE (NameEn),
    CONSTRAINT UK_BloodType_NameEs UNIQUE (NameEs),
    CONSTRAINT UK_BloodType_Display UNIQUE (Display)
)WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

INSERT INTO olbil.WardGender(WardGenderId, Name)
VALUES
(1, 'Unisex'),
(2, 'Femenine'),
(3, 'Masculine')
ON CONFLICT DO NOTHING;

SELECT SETVAL(pg_get_serial_sequence('olbil.wardgender', 'wardgenderid'),
              (SELECT MAX(WardGenderId) FROM olbil.WardGender)
);

INSERT INTO olbil.WardStatus(WardStatusId, Name)
VALUES
(1, 'Not Available'),
(2, 'BedsAvailable'),
(3, 'Full')
ON CONFLICT DO NOTHING;

SELECT SETVAL(
        pg_get_serial_sequence('olbil.wardstatus', 'wardstatusid'),
        (SELECT MAX(WardStatusId) FROM olbil.WardStatus)
);

INSERT INTO olbil.BedStatus(BedStatusId, Name)
VALUES
(1, 'Free'),
(2, 'Occupied'),
(3, 'Reserved')
ON CONFLICT DO NOTHING;

SELECT SETVAL(
        pg_get_serial_sequence('olbil.bedstatus', 'bedstatusid'),
        (SELECT MAX(BedStatusId) FROM olbil.BedStatus)
);

INSERT INTO olbil.BloodType(BloodTypeId, NameEn, NameEs, Display)
VALUES
(1, 'O Negative', 'O Negativo', 'O-'),
(2, 'O Positive', 'O Positivo', 'O+'),
(3, 'A Negative', 'A Negativo', 'A-'),
(4, 'A Positive', 'A Positivo', 'A+'),
(5, 'B Negative', 'B Negativo', 'B-'),
(6, 'B Positive', 'B Positivo', 'B+'),
(7, 'AB Negative', 'AB Negativo', 'AB-'),
(8, 'AB Positive', 'AB Positivo', 'AB+')
ON CONFLICT DO NOTHING;

SELECT SETVAL(
        pg_get_serial_sequence('olbil.bloodtype', 'bloodtypeid'),
        (SELECT MAX(BloodTypeId) FROM olbil.BloodType)
);


INSERT INTO olbil.AppointmentReason(AppointmentReasonId, Description)
VALUES
(01, 'Enfermedad Aguda'),
(02, 'Seguimiento'),
(03, 'Quimioterapia'),
(04, 'En Estudio/Proceso de diagnostico'),
(05, 'Hospitalizacion'),
(06, 'Paciente Nuevo'),
(07, 'Paliativo'),
(08, 'En Tratamiento'),
(09, 'En Observación'),
(10, 'Prolongación de vida')
ON CONFLICT DO NOTHING;

SELECT SETVAL(
        pg_get_serial_sequence('olbil.appointmentreason', 'appointmentreasonid'),
        (SELECT MAX(AppointmentReasonId) FROM olbil.AppointmentReason)
);

INSERT INTO olbil.MedicalSpecialty(MedicalSpecialtyId, Description)
VALUES
(01, 'Oncología'),
(02, 'Enfermeria'),
(03, 'Cardiología'),
(04, 'Dermatología'),
(04, 'Endocrinología'),
(05, 'Gastroenterología'),
(06, 'Hematología'),
(07, 'Infectología'),
(08, 'Nefrología'),
(09, 'Neurología'),
(10, 'Nutrición'),
(11, 'Odontología'),
(12, 'Oftalmología'),
(13, 'Ortopedia'),
(14, 'Otorrinolaringología'),
(15, 'Psicología'),
(16, 'Oncólogia de radiación'),
(17, 'Trabajo Social'),
(18, 'Cirugía general'),
(19, 'Cirugía subespecializada'),
(20, 'Neumología'),
(21, 'Psiquiatría')
ON CONFLICT DO NOTHING;

SELECT SETVAL(
        pg_get_serial_sequence('olbil.medicalspecialty', 'medicalspecialtyid'),
        (SELECT MAX(medicalspecialtyid) FROM olbil.medicalspecialty)
);
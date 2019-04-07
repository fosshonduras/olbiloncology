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

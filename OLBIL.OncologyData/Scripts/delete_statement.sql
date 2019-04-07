DELETE FROM "OncologyPatients" OP
USING "Person" P WHERE P."PersonId" = OP."PersonId"
AND P."GovernmentIDNumber" LIKE '0101203%';

DELETE FROM "Person" P
WHERE P."GovernmentIDNumber" LIKE '0101203%'
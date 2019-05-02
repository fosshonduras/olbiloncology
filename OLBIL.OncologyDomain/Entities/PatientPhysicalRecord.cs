using System.Collections.Generic;

namespace OLBIL.OncologyDomain.Entities
{
    public class PatientPhysicalRecord : BaseEntity
    {
        public int PatientPhysicalRecordId { get; set; }
        public int OncologyPatientId { get; set; }
        public int RecordStorageLocationId { get; set; }
        public string RecordNumber { get; set; }
        public RecordStorageLocation RecordStorageLocation { get; set; }
        public virtual ICollection<PhysicalRecordTransfer> Transactions { get; set; }
        public virtual OncologyPatient OncologyPatient { get; set; }
    }
}

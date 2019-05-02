using System;
using System.Collections.Generic;
using System.Text;

namespace OLBIL.OncologyDomain.Entities
{
    public class PhysicalRecordTransfer : BaseEntity
    {
        public int PhysicalRecordTransferId { get; set; }
        public int PatientPhysicalRecordId { get; set; }
        public int TargetLocationId { get; set; }
        public string DeliveredBy { get; set; }
        public string ReceivedBy { get; set; }
        public DateTime Date { get; set; }


        public virtual PatientPhysicalRecord PatientPhysicalRecord { get; set; }
        public virtual RecordStorageLocation TargetLocation { get; set; }
    }
}

using System;

namespace OLBIL.OncologyApplication.Models
{
    public class PhysicalRecordTransferModel
    {
        public int? PhysicalRecordTransferId { get; set; }
        public int? PatientPhysicalRecordId { get; set; }
        public int? TargetLocationId { get; set; }
        public string DeliveredBy { get; set; }
        public string ReceivedBy { get; set; }
        public DateTime? Date { get; set; }
    }
}

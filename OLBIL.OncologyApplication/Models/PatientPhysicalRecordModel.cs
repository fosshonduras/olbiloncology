namespace OLBIL.OncologyApplication.Models
{
    public class PatientPhysicalRecordModel
    {
        public int? PatientPhysicalRecordId { get; set; }
        public int? OncologyPatientId { get; set; }
        public int? RecordStorageLocationId { get; set; }
        public string RecordNumber { get; set; }
    }
}

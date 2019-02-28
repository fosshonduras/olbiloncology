namespace OLBIL.OncologyWebApp.Entities
{
    public class Appointment : BaseEntity
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int Status { get; set; }

        public virtual OncologyPatient Patient { get; set; }
    }
}

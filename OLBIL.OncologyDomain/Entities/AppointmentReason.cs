namespace OLBIL.OncologyDomain.Entities
{
    public class AppointmentReason : BaseEntity
    {
        public int AppointmentReasonId { get; set; }
        public string Description { get; set; }
    }
}
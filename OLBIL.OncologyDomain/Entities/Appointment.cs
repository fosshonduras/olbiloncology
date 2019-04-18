using OLBIL.OncologyDomain.Enums;
using System;

namespace OLBIL.OncologyDomain.Entities
{
    public class Appointment : BaseEntity
    {
        public int AppointmentId { get; set; }
        public int OncologyPatientId { get; set; }
        public int? HealthProfessionalId { get; set; }
        public AppointmentStatus AppointmentStatusId { get; set; }
        public DateTime Date { get; set; }
        public string AttentionBlocks { get; set; }
        public bool PatientAttended { get; set; }
        public int? RescheduledAppointmentId { get; set; }
        public int? AppointmentReasonId { get; set; }
        public string Notes { get; set; }
        public string SpecialNotes { get; set; }

        public virtual OncologyPatient OncologyPatient { get; set; }
        public virtual HealthProfessional HealthProfessional { get; set; }
        public virtual Appointment RescheduledAppointment { get; set; }
        public virtual AppointmentReason AppointmentReason { get; set; }

    }
}

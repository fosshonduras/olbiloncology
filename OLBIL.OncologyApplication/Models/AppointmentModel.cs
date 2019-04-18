using OLBIL.OncologyDomain.Enums;
using System;

namespace OLBIL.OncologyApplication.Models
{
    public class AppointmentModel
    {
        public int? AppointmentId { get; set; }
        public int? OncologyPatientId { get; set; }
        public int? HealthProfessionalId { get; set; }
        public AppointmentStatus? AppointmentStatusId { get; set; }
        public string AppointmentStatusName { get; set; }
        public DateTime? Date { get; set; }
        public string AttentionBlocks { get; set; }
        public bool PatientAttended { get; set; }
        public int? RescheduledAppointmentId { get; set; }
        public int? AppointmentReasonId { get; set; }
        public string Notes { get; set; }
        public string SpecialNotes { get; set; }
    }
}

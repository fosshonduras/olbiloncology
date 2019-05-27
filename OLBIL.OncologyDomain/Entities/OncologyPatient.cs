using System;
using System.Collections.Generic;

namespace OLBIL.OncologyDomain.Entities
{
    public class OncologyPatient : BaseEntity
    {
        public OncologyPatient()
        {
            Appointments = new HashSet<Appointment>();
            BloodTransfusions = new HashSet<BloodTransfusion>();
        }

        public int OncologyPatientId { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public string InformantsRelationship { get; set; }
        // Oncology
        public string ReasonForReferral { get; set; }
        public int? MainDiagnosisId { get; set; }

        public Guid? PersonId { get; set; }
        public Person Person { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<EvolutionCard> EvolutionCards { get; set; }
        public virtual Diagnosis MainDiagnosis { get; set; }
        public virtual PatientPhysicalRecord PatientPhysicalRecord { get; set; }
        public virtual ICollection<BloodTransfusion> BloodTransfusions { get; set; }
    }
}

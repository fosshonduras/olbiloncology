using System;

namespace OLBIL.OncologyDomain.Entities
{
    public class AmbulatoryAttentionRecord: BaseEntity
    {
        public int AmbulatoryAttentionRecordId { get; set; }
        public int HealthProfessionalId { get; set; }
        public int OncologyPatientId { get; set; }
        public bool IsNewPatient { get; set; } = false;
        public int DiagnosisId { get; set; }
        public string TreatmentPhase { get; set; }
        public string DiseaseEventDescription { get; set; }
        public DateTime? NextAppointmentDate { get; set; }
        public DateTime Date { get; set; }
        public string ReferredTo { get; set; }
        public string ReceivedFrom { get; set; }

        public virtual HealthProfessional HealthProfessional { get; set; }
        public virtual OncologyPatient OncologyPatient { get; set; }
        public virtual Diagnosis Diagnosis { get; set; }
    }
}

using AutoMapper;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyDomain.Entities;
using System;

namespace OLBIL.OncologyApplication.Models
{
    public class AmbulatoryAttentionRecordModel : IHaveCustomMapping
    {
        public int? AmbulatoryAttentionRecordId { get; set; }
        public int? HealthProfessionalId { get; set; }
        public int? OncologyPatientId { get; set; }
        public bool IsNewPatient { get; set; } = false;
        public int? DiagnosisId { get; set; }
        public string TreatmentPhase { get; set; }
        public string DiseaseEventDescription { get; set; }
        public DateTime? NextAppointmentDate { get; set; }
        public DateTime? Date { get; set; }
        public string ReferredTo { get; set; }
        public string ReceivedFrom { get; set; }

        
        public string HealthProfessionalFullName { get; set; }
        public string OncologyPatientFullName { get; set; }
        public string DiagnosisName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<AmbulatoryAttentionRecord, AmbulatoryAttentionRecordModel>()
                .ForMember(cDTO => cDTO.DiagnosisName, opt => opt.MapFrom(c => c.Diagnosis.ShortDescriptor.ToString()))
                .ForMember(cDTO => cDTO.HealthProfessionalFullName, opt => opt.MapFrom(c => c.HealthProfessional.Person.FullName))
                .ForMember(cDTO => cDTO.OncologyPatientFullName, opt => opt.MapFrom(c => c.OncologyPatient.Person.FullName))
                ;
        }
    }
}

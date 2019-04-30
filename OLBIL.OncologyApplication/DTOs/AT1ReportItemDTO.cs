using AutoMapper;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;

namespace OLBIL.OncologyApplication.DTOs
{
    public class AT1ReportItemDTO: AmbulatoryAttentionRecordModel, IHaveCustomMapping
    {
        public string HealthProfessionalFullName { get; set; }
        public string OncologyPatientFullName { get; set; }
        public string DiagnosisName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<AmbulatoryAttentionRecord, AT1ReportItemDTO>()
                .ForMember(cDTO => cDTO.DiagnosisName, opt => opt.MapFrom(c => c.Diagnosis.ShortDescriptor.ToString()))
                .ForMember(cDTO => cDTO.HealthProfessionalFullName , opt => opt.MapFrom(c => c.HealthProfessional.Person.FullName))
                .ForMember(cDTO => cDTO.OncologyPatientFullName, opt => opt.MapFrom(c => c.OncologyPatient.Person.FullName))
                ;
        }
    }
}

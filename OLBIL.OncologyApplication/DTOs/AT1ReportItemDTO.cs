using AutoMapper;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System;

namespace OLBIL.OncologyApplication.DTOs
{
    public class AT1ReportItemDTO : AmbulatoryAttentionRecordModel, IHaveCustomMapping
    {
        public string OncologyPatientGovernmentIDNumber { get; set; }
        public string PatientPhysicalRecordNumber { get; set; }
        public string Gender { get; set; }
        public DateTime? Birthdate { get; set; }
        public string ProcedenceDivisionLevel2 { get; set; }
        public string ProcedenceDivisionLevel3 { get; set; }
        public string ProcedenceDivisionLevel4 { get; set; }
        public int AgeInYears { get; set; }
        public int AgeInMonthsOverYears { get; set; }
        public int AgeInDaysOverMonths { get; set; }

        public new void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<AmbulatoryAttentionRecord, AT1ReportItemDTO>()
                .ForMember(cDTO => cDTO.DiagnosisName, opt => opt.MapFrom(c => c.Diagnosis.ShortDescriptor.ToString()))
                .ForMember(cDTO => cDTO.HealthProfessionalFullName, opt => opt.MapFrom(c => c.HealthProfessional.Person.FullName))
                .ForMember(cDTO => cDTO.OncologyPatientFullName, opt => opt.MapFrom(c => c.OncologyPatient.Person.FullName))
                .ForMember(cDTO => cDTO.PatientPhysicalRecordNumber, opt => opt.MapFrom(c => c.OncologyPatient.PatientPhysicalRecord.RecordNumber))
                .ForMember(cDTO => cDTO.OncologyPatientGovernmentIDNumber, opt => opt.MapFrom(c => c.OncologyPatient.Person.GovernmentIDNumber))
                .ForMember(cDTO => cDTO.Gender, opt => opt.MapFrom(c => c.OncologyPatient.Person.Gender))
                .ForMember(cDTO => cDTO.Birthdate, opt => opt.MapFrom(c => c.OncologyPatient.Person.Birthdate))
                .ForMember(cDTO => cDTO.ProcedenceDivisionLevel2, opt => opt.MapFrom(c => c.OncologyPatient.Person.State))
                ;
        }
    }
}

using AutoMapper;
using OLBIL.OncologyDomain.Entities;
using System;

namespace OLBIL.OncologyApplication.Models
{
    public class EvolutionCardModel
    {
        public int? EvolutionCardId { get; set; }
        public int? OncologyPatientId { get; set; }
        public int? AppointmentId { get; set; }
        public decimal? HeightCm { get; set; }
        public decimal? WeightKg { get; set; }
        public decimal? TemperatureC { get; set; }
        public int? HeartBeatRateBpm { get; set; }
        
        public int? DiagnosisId { get; set; }
        public string Directions { get; set; }
        public string Observations { get; set; }
        public DateTime? NextAppointmentDate { get; set; }
        public string ReferredTo { get; set; }
        public int? HealthProfessionalId { get; set; }
        public decimal? BodyMassIndex { get; set; }

        public string HealthProfessionalFullName { get; set; }
        public string OncologyPatientFullName { get; set; }
        public string DiagnosisShortDescriptor { get; set; }
        public DateTime? AppointmentDate { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<EvolutionCard, EvolutionCardModel>()
                .ForMember(cDTO => cDTO.DiagnosisShortDescriptor, opt => opt.MapFrom(c => c.Diagnosis.ShortDescriptor.ToString()))
                .ForMember(cDTO => cDTO.HealthProfessionalFullName, opt => opt.MapFrom(c => c.HealthProfessional.Person.FullName))
                .ForMember(cDTO => cDTO.OncologyPatientFullName, opt => opt.MapFrom(c => c.OncologyPatient.Person.FullName))
                .ForMember(cDTO => cDTO.AppointmentDate, opt => opt.MapFrom(c => c.Appointment == null ? (DateTime?)null : c.Appointment.Date))
                ;
        }
    }
}

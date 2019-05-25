using AutoMapper;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyDomain.Entities;
using OLBIL.OncologyDomain.Enums;
using System;

namespace OLBIL.OncologyApplication.Models
{
    public class AppointmentModel : IHaveCustomMapping
    {
        public int? AppointmentId { get; set; }
        public int? OncologyPatientId { get; set; }
        public int? HealthProfessionalId { get; set; }
        public AppointmentStatus? AppointmentStatusId { get; set; }
        public DateTime? Date { get; set; }
        public string AttentionBlocks { get; set; }
        public bool PatientAttended { get; set; }
        public int? RescheduledAppointmentId { get; set; }
        public int? AppointmentReasonId { get; set; }
        public string Notes { get; set; }
        public string SpecialNotes { get; set; }

        public string OncologyPatientFullName { get; set; }
        public string HealthProfessionalFullName { get; set; }
        public string AppointmentStatusName { get; set; }
        public string AppointmentReasonName { get; set; }
        public DateTime? RescheduledAppointmentDate { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Appointment, AppointmentModel>()
                .ForMember(cDTO => cDTO.AppointmentReasonName, opt => opt.MapFrom(c => c.AppointmentReason.Description))
                .ForMember(cDTO => cDTO.AppointmentStatusName, opt => opt.MapFrom(c => c.AppointmentStatusId.ToString()))
                .ForMember(cDTO => cDTO.HealthProfessionalFullName, opt => opt.MapFrom(c => c.HealthProfessional.Person.FullName))
                .ForMember(cDTO => cDTO.OncologyPatientFullName, opt => opt.MapFrom(c => c.OncologyPatient.Person.FullName))
                .ForMember(cDTO => cDTO.RescheduledAppointmentDate, opt => opt.MapFrom(c => c.RescheduledAppointment == null? (DateTime?)null : c.RescheduledAppointment.Date))

                ;
        }
    }
}

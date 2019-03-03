using AutoMapper;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyCore.Entities;
using System;

namespace OLBIL.OncologyApplication.Models
{
    public class OncologyPatientModel: IHaveCustomMapping
    {
        public int OncologyPatientId { get; set; }

        public PersonModel Person { get; set; }
        
        public DateTime? RegistrationDate { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public string InformantsRelationship { get; set; }

        // Oncology
        public string ReasonForReferral { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<OncologyPatient, OncologyPatientModel>()
                .ForMember(cDTO => cDTO.Person, opt => opt.MapFrom(c => c.Person));
        }
    }
}

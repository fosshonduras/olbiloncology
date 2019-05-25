using AutoMapper;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyDomain.Entities;
using System;

namespace OLBIL.OncologyApplication.Models
{
    public class HealthProfessionalModel : IHaveCustomMapping
    {
        public int? HealthProfessionalId { get; set; }
        public Guid? PersonId { get; set; }
        public PersonModel Person { get; set; }
        public int? MainSpecialtyId { get; set; }

        public string MainSpecialtyName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<HealthProfessional, HealthProfessionalModel>()
                .ForMember(cDTO => cDTO.MainSpecialtyName, opt => opt.MapFrom(c => c.MainSpecialty.Description))
                ;
        }
    }
}

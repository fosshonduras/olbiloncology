using AutoMapper;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyDomain.Entities;
using System;

namespace OLBIL.OncologyApplication.Models
{
    public class BloodTransfusionModel : IHaveCustomMapping
    {
        public int? BloodTransfusionId { get; set; }
        public int? OncologyPatientId { get; set; }
        public int? WardId { get; set; }
        public string VerifyBy { get; set; }
        public DateTime? Date { get; set; }
        public string Group { get; set; }
        public string ABORH { get; set; }

        public string WardName { get; set; }
        public string OncologyPatientFullName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<BloodTransfusion, BloodTransfusionModel>()
                .ForMember(cDTO => cDTO.WardName, opt => opt.MapFrom(c => c.Ward.Name))
                .ForMember(cDTO => cDTO.OncologyPatientFullName, opt => opt.MapFrom(c => c.OncologyPatient.Person.FullName))
                ;
        }
    }
}

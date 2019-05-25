using AutoMapper;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyDomain.Entities;

namespace OLBIL.OncologyApplication.Models
{
    public class AdministrativeDivisionModel : IHaveCustomMapping
    {
        public int AdministrativeDivisionId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int? ParentId { get; set; }

        public string ParentName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<AdministrativeDivision, AdministrativeDivisionModel>()
                .ForMember(cDTO => cDTO.ParentName, opt => opt.MapFrom(c => c.Parent.Name))
                ;
        }
    }
}

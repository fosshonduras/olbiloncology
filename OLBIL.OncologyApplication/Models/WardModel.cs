using AutoMapper;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyDomain.Entities;
using OLBIL.OncologyDomain.Enums;

namespace OLBIL.OncologyApplication.Models
{
    public class WardModel: IHaveCustomMapping
    {
        public int? WardId { get; set; }
        public string Name { get; set; }
        public int? BuildingId { get; set; }
        public int? FloorNumber { get; set; }
        public int? HospitalUnitId { get; set; }
        public WardGender? WardGenderId { get; set; }
        public WardStatus? WardStatusId { get; set; }

        public string BuildingName { get; set; }
        public string HospitalUnitName { get; set; }
        public string WardGenderName { get; set; }
        public string WardStatusName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Ward, WardModel>()
                .ForMember(cDTO => cDTO.WardStatusName, opt => opt.MapFrom(e => e.WardStatusId.ToString()))
                .ForMember(cDTO => cDTO.WardGenderName, opt => opt.MapFrom(e => e.WardGenderId.ToString()))
                .ForMember(cDTO => cDTO.BuildingName, opt => opt.MapFrom(c => c.Building.Name))
                .ForMember(cDTO => cDTO.HospitalUnitName, opt => opt.MapFrom(c => c.Unit.Name))
                ;
        }
    }
}

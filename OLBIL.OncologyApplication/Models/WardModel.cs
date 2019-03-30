using AutoMapper;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyCore.Entities;

namespace OLBIL.OncologyApplication.Models
{
    public class WardModel: IHaveCustomMapping
    {
        public int? WardId { get; set; }
        public string Name { get; set; }
        public int? BuildingId { get; set; }
        public int? FloorNumber { get; set; }
        public int? HospitalUnitId { get; set; }
        public string WardGenderName { get; set; }
        public string WardStatusName { get; set; }
        public WardGender? WardGenderId { get; set; }
        public WardStatus? WardStatusId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Ward, WardModel>()
                .ForMember(cDTO => cDTO.WardStatusName, opt => opt.MapFrom(e => e.WardStatusId.ToString()))
                .ForMember(cDTO => cDTO.WardGenderName, opt => opt.MapFrom(e => e.WardGenderId.ToString()));
        }
    }
}

using AutoMapper;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyDomain.Entities;

namespace OLBIL.OncologyApplication.Models
{
    public class RecordStorageLocationModel : IHaveCustomMapping
    {
        public int? RecordStorageLocationId { get; set; }
        public string Name { get; set; }
        public int? ParentLocationId { get; set; }

        public string ParentLocationName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<RecordStorageLocation, RecordStorageLocationModel>()
                .ForMember(cDTO => cDTO.ParentLocationName, opt => opt.MapFrom(c => c.ParentLocation.Name))
                ;
        }
    }
}

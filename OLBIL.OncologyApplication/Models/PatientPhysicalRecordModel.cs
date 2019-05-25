using AutoMapper;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyDomain.Entities;

namespace OLBIL.OncologyApplication.Models
{
    public class PatientPhysicalRecordModel : IHaveCustomMapping
    {
        public int? PatientPhysicalRecordId { get; set; }
        public int? OncologyPatientId { get; set; }
        public int? RecordStorageLocationId { get; set; }
        public string RecordNumber { get; set; }

        public string RecordStorageLocationName { get; set; }
        public string OncologyPatientFullName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<PatientPhysicalRecord, PatientPhysicalRecordModel>()
                .ForMember(cDTO => cDTO.RecordStorageLocationName, opt => opt.MapFrom(c => c.RecordStorageLocation.Name))
                .ForMember(cDTO => cDTO.OncologyPatientFullName, opt => opt.MapFrom(c => c.OncologyPatient.Person.FullName))
                ;
        }
    }
}

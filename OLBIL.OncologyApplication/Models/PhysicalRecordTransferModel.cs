using AutoMapper;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyDomain.Entities;
using System;

namespace OLBIL.OncologyApplication.Models
{
    public class PhysicalRecordTransferModel : IHaveCustomMapping
    {
        public int? PhysicalRecordTransferId { get; set; }
        public int? PatientPhysicalRecordId { get; set; }
        public int? TargetLocationId { get; set; }
        public string DeliveredBy { get; set; }
        public string ReceivedBy { get; set; }
        public DateTime? Date { get; set; }

        public string PatientPhysicalRecordNumber { get; set; }
        public string TargetLocationName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap < PhysicalRecordTransfer, PhysicalRecordTransferModel>()
                .ForMember(cDTO => cDTO.PatientPhysicalRecordNumber, opt => opt.MapFrom(c => c.PatientPhysicalRecord.RecordNumber))
                .ForMember(cDTO => cDTO.TargetLocationName, opt => opt.MapFrom(c => c.TargetLocation.Name))
                ;
        }
    }
}

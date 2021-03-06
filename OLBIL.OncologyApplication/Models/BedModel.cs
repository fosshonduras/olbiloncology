﻿using AutoMapper;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyDomain.Entities;
using OLBIL.OncologyDomain.Enums;

namespace OLBIL.OncologyApplication.Models
{
    public class BedModel: IHaveCustomMapping
    {
        public int? BedId { get; set; }
        public string Name { get; set; }
        public string LongDescription { get; set; }
        public int? WardId { get; set; }
        public BedStatus? BedStatusId { get; set; }

        public string WardName { get; set; }
        public string BedStatusName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Bed, BedModel>()
                .ForMember(cDTO => cDTO.BedStatusName, opt => opt.MapFrom(c => c.BedStatusId.ToString()))
                .ForMember(cDTO => cDTO.WardName, opt => opt.MapFrom(c => c.Ward.Name))
                ;
        }
    }
}

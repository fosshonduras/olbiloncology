using AutoMapper;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyDomain.Entities;

namespace OLBIL.OncologyApplication.Models
{
    public class TransfusionVitalSignsDetailModel : IHaveCustomMapping
    {
        public int? TransfusionVitalSignsDetailId { get; set; }
        public int? BloodTransfusionId { get; set; }
        public int? TransfusionPhaseId { get; set; }
        public decimal? ArterialPressure { get; set; }
        public decimal? TemperatureC { get; set; }
        public decimal? HeartbeatRateBpm { get; set; }
        public decimal? RespiratoryFrequence { get; set; }
        public string Responsible { get; set; }
        public string Observations { get; set; }

        public string TransfusionPhaseName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<TransfusionVitalSignsDetail, TransfusionVitalSignsDetailModel>()
                .ForMember(cDTO => cDTO.TransfusionPhaseName, opt => opt.MapFrom(c => c.TransfusionPhase.Name))
                ;
        }
    }
}

namespace OLBIL.OncologyDomain.Entities
{
    public class TransfusionVitalSignsDetail
    {
        public int TransfusionVitalSignsDetailId { get; set; }
        public int BloodTransfusionId { get; set; }
        public int TransfusionPhaseId { get; set; }
        public decimal ArterialPressure { get; set; }
        public decimal TemperatureC { get; set; }
        public decimal HeartbeatRateBpm { get; set; }
        public decimal RespiratoryFrequence { get; set; }
        public string Responsible { get; set; }
        public string Observations { get; set; }

        public virtual BloodTransfusion BloodTransfusion { get; set; }
        public virtual TransfusionPhase TransfusionPhase { get; set; }
    }
}

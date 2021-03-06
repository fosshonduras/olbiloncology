﻿using System;

namespace OLBIL.OncologyDomain.Entities
{
    public class EvolutionCard : BaseEntity
    {
        public int EvolutionCardId { get; set; }
        public int OncologyPatientId { get; set; }
        public int? AppointmentId { get; set; }
        public int? HealthProfessionalId { get; set; }
        public decimal? HeightCm { get; set; }
        public decimal? WeightKg { get; set; }
        public decimal? BodyMassIndex => HeightCm == null || HeightCm == 0 ? 0 : ( (WeightKg ?? 0) / (HeightCm*HeightCm) ) * 10000;
        public decimal? TemperatureC { get; set; }
        public int? HeartBeatRateBpm { get; set; }
        public int? DiagnosisId { get; set; }
        public string Directions { get; set; }
        public string Observations { get; set; }
        public DateTime? NextAppointmentDate { get; set; }
        public string ReferredTo { get; set; }

        public virtual OncologyPatient OncologyPatient { get; set; }
        public virtual HealthProfessional HealthProfessional { get; set; }
        public virtual Appointment Appointment { get; set; }
        public virtual Diagnosis Diagnosis { get; set; }
    }
}

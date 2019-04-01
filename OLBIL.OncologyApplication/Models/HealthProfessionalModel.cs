using System;

namespace OLBIL.OncologyApplication.Models
{
    public class HealthProfessionalModel
    {
        public int? HealthProfessionalId { get; set; }
        public Guid? PersonId { get; set; }
        public PersonModel Person { get; set; }
    }
}

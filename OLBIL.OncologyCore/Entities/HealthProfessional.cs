using System;

namespace OLBIL.OncologyDomain.Entities
{
    public class HealthProfessional : BaseEntity
    {
        public int HealthProfessionalId { get; set; }
        public Guid? PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}

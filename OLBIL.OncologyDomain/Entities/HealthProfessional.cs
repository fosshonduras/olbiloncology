using System;
using System.Collections.Generic;

namespace OLBIL.OncologyDomain.Entities
{
    public class HealthProfessional : BaseEntity
    {
        public int HealthProfessionalId { get; set; }
        public Guid? PersonId { get; set; }
        public int? MainSpecialtyId { get; set; }

        public virtual Person Person { get; set; }
        public virtual MedicalSpecialty MainSpecialty { get; set; }
        public virtual ICollection<HealthProfessionalMedicalSpecialty> MedicalSpecialties { get; set; }
    }
}

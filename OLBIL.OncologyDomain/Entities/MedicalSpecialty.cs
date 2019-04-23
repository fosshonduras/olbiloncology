using System.Collections.Generic;

namespace OLBIL.OncologyDomain.Entities
{
    public class MedicalSpecialty : BaseEntity
    {
        public int MedicalSpecialtyId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<HealthProfessionalMedicalSpecialty> HealthProfessionalWithSpecialty { get; set; }
    }
}

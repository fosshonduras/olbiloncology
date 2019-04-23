namespace OLBIL.OncologyDomain.Entities
{
    public class HealthProfessionalMedicalSpecialty: BaseEntity
    {
        public int HealthProfessionalMedicalSpecialtyId { get; set; }
        public int HealthProfessionalId { get; set; }
        public int MedicalSpecialtyId { get; set; }

        public virtual HealthProfessional HealthProfessional { get; set; }
        public virtual MedicalSpecialty MedicalSpecialty { get; set; }
    }
}

namespace OLBIL.OncologyDomain.Entities
{
    public class Diagnosis : BaseEntity
    {
        public int DiagnosisId { get; set; }
        public string ICDCode { get; set; }
        public string CompleteDescriptor { get; set; }
        public string ShortDescriptor { get; set; }
    }
}

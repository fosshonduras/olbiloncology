namespace OLBIL.OncologyApplication.Models
{
    public class DiagnosisModel
    {
        public int? DiagnosisId { get; set; }
        public string ICDCode { get; set; }
        public string CompleteDescriptor { get; set; }
        public string ShortDescriptor { get; set; }
    }
}

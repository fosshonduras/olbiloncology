using System;

namespace OLBIL.OncologyWebApp.Models
{
    public class OncologyPatientModel
    {
        public int OncologyPatientId { get; set; }

        public PersonModel Person { get; set; }
        
        public DateTime? RegistrationDate { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public string InformantsRelationship { get; set; }

        // Oncology
        public string ReasonForReferral { get; set; }
    }
}

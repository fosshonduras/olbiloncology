using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace OLBIL.OncologyWebApp.Entities
{
    public class OncologyPatient : BaseEntity
    {
        public OncologyPatient()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int OncologyPatientId { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public string InformantsRelationship { get; set; }
        // Oncology
        public string ReasonForReferral { get; set; }

        public Guid? PersonId { get; set; }
        public Person Person { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}

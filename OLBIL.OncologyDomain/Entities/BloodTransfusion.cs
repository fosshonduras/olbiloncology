using System;
using System.Collections.Generic;

namespace OLBIL.OncologyDomain.Entities
{
    public class BloodTransfusion : BaseEntity
    {
        public BloodTransfusion()
        {
            TransfusionProductDetails = new HashSet<TransfusionProductDetail>();
            TransfusionVitalSignsDetails = new HashSet<TransfusionVitalSignsDetail>();
        }

        public int BloodTransfusionId { get; set; }
        public int OncologyPatientId { get; set; }
        public int WardId { get; set; }
        public string VerifyBy { get; set; }
        public DateTime Date { get; set; }
        public string Group { get; set; }
        public string ABORH { get; set; }

        public virtual OncologyPatient OncologyPatient { get; set; }
        public virtual Ward Ward { get; set; }

        public virtual ICollection<TransfusionProductDetail> TransfusionProductDetails { get; set; }
        public virtual ICollection<TransfusionVitalSignsDetail> TransfusionVitalSignsDetails { get; set; }
    }
}

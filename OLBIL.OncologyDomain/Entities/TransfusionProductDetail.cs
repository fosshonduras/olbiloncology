using System;

namespace OLBIL.OncologyDomain.Entities
{
    public class TransfusionProductDetail : BaseEntity
    {
        public int TransfusionProductDetailId { get; set; }
        public int BloodTransfusionId { get; set; }
        public string UnitNumber { get; set; }
        public string Component { get; set; }
        public decimal Quantity { get; set; }
        public string ABORH { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Responsible { get; set; }
        public string AdverseReactions { get; set; }

        public virtual BloodTransfusion BloodTransfusion { get; set; }
    }
}

using OLBIL.OncologyDomain.Enums;
using System.Collections.Generic;

namespace OLBIL.OncologyDomain.Entities
{
    public class Ward : BaseEntity
    {
        public Ward()
        {
            Beds = new HashSet<Bed>();
            BloodTransfusions = new HashSet<BloodTransfusion>();
        }

        public int WardId { get; set; }
        public string Name { get; set; }
        public int BuildingId { get; set; }
        public int FloorNumber { get; set; }
        public int HospitalUnitId { get; set; }
        public WardGender WardGenderId { get; set; }
        public WardStatus WardStatusId { get; set; }

        public virtual Building Building { get; set; }
        public virtual HospitalUnit Unit { get; set; }
        public virtual ICollection<Bed> Beds { get; set; }
        public virtual ICollection<BloodTransfusion> BloodTransfusions { get; set; }
    }
}

using System.Collections.Generic;

namespace OLBIL.OncologyCore.Entities
{
    public class Ward : BaseEntity
    {
        public Ward()
        {
            Beds = new HashSet<Bed>();
        }

        public int WardId { get; set; }
        public string Name { get; set; }
        public int BuildingId { get; set; }
        public int FloorNumber { get; set; }
        public int UnitId { get; set; }
        public WardGender WardGenderId { get; set; }
        public WardStatus WardStatusId { get; set; }

        public virtual Building Building { get; set; }
        public virtual HospitalUnit Unit { get; set; }
        public virtual ICollection<Bed> Beds { get; set; }
    }
}

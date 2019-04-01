using System.Collections.Generic;

namespace OLBIL.OncologyDomain.Entities
{
    public class HospitalUnit : BaseEntity
    {
        public HospitalUnit()
        {
            Wards = new HashSet<Ward>();
        }
        public int HospitalUnitId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Ward> Wards { get; set; }
    }
}

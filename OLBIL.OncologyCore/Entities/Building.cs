using System.Collections.Generic;

namespace OLBIL.OncologyCore.Entities
{
    public class Building : BaseEntity
    {
        public Building()
        {
            Wards = new HashSet<Ward>();
        }
        public int BuildingId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Ward> Wards { get; set; }
    }
}

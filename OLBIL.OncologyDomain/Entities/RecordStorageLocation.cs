using System.Collections.Generic;

namespace OLBIL.OncologyDomain.Entities
{
    public class RecordStorageLocation: BaseEntity
    {
        public int RecordStorageLocationId { get; set; }
        public string Name { get; set; }
        public int ParentLocationId { get; set; }
        public virtual RecordStorageLocation ParentLocation { get; set; }
        public virtual ICollection<PhysicalRecordTransfer> Transfers { get; set; }
    }
}

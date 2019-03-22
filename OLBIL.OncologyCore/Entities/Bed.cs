using System.ComponentModel.DataAnnotations.Schema;

namespace OLBIL.OncologyCore.Entities
{
    public class Bed : BaseEntity
    {
        public int BedId { get; set; }
        public string Name { get; set; }
        public string LongDescription { get; set; }
        public int WardId { get; set; }

        public BedStatus BedStatusId { get; set; }

        public virtual Ward Ward { get; set; }
        //[NotMapped]
        //public BedStatus BedStatus
        //{
        //    get
        //    {
        //        return (BedStatus)BedStatusId;
        //    }
        //    set
        //    {
        //        BedStatusId = (int)value;
        //    }
        //}
    }
}

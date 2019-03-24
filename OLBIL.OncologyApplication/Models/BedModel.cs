using OLBIL.OncologyCore.Entities;

namespace OLBIL.OncologyApplication.Models
{
    public class BedModel
    {
        public int BedId { get; set; }
        public string Name { get; set; }
        public string LongDescription { get; set; }
        public int WardId { get; set; }
        public BedStatus BedStatusId { get; set; }
        public string BedStatusName { get; set; }
    }
}

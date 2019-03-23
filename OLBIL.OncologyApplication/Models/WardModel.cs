using OLBIL.OncologyCore.Entities;

namespace OLBIL.OncologyApplication.Models
{
    public class WardModel
    {
         public int WardId { get; set; }
        public string Name { get; set; }
        public int BuildingId { get; set; }
        public int FloorNumber { get; set; }
        public int UnitId { get; set; }
        public string WardGenderName { get; set; }
        public string WardStatusName { get; set; }
        public WardGender WardGenderId { get; set; }
        public WardStatus WardStatusId { get; set; }
    }
}

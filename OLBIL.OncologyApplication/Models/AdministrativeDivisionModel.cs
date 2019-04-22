namespace OLBIL.OncologyApplication.Models
{
    public class AdministrativeDivisionModel
    {
        public int AdministrativeDivisionId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
        public int Level { get; set; }
        public int? ParentId { get; set; }
    }
}

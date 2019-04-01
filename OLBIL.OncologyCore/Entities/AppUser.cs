using System;

namespace OLBIL.OncologyDomain.Entities
{
    public class AppUser: BaseEntity
    {
        public Guid AppUserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

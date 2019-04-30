using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OLBIL.OncologyDomain.Entities
{
    public class Person : BaseEntity
    {
        public Guid PersonId { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string AdditionalLastName { get; set; }
        public string PreferredName { get; set; }
        public string TemporaryIdNumber { get; set; }
        public string GovernmentIDNumber { get; set; }
        public string Address { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }

        public string Nationality { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Birthplace { get; set; }
        public string FamilyStatus { get; set; }
        public string SchoolLevel { get; set; }
        public string MethodOfTranspotation { get; set; }

        public Guid? AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {MiddleName ?? string.Empty} {LastName ?? string.Empty} {AdditionalLastName ?? string.Empty}";
    }
}

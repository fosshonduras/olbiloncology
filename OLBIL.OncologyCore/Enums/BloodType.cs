using System.ComponentModel.DataAnnotations;

namespace OLBIL.OncologyDomain.Enums
{
    public enum BloodType
    {
        [Display(Name = "O-")]
        ONegative,
        [Display(Name = "O+")]
        OPositive,
        [Display(Name = "A-")]
        ANegative,
        [Display(Name = "A+")]
        APositive,
        [Display(Name = "B-")]
        BNegative,
        [Display(Name = "B+")]
        BPositive,
        [Display(Name = "AB-")]
        ABNegative,
        [Display(Name = "AB+")]
        ABPositive
    }
}

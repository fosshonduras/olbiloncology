using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyDomain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Interfaces
{
    public interface IOncologyContext
    {
        DbSet<MedicalSpecialty> MedicalSpecialties { get; set; }
        DbSet<Diagnosis> Diagnoses { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<AdministrativeDivision> AdministrativeDivisions { get; set; }
        DbSet<OncologyPatient> OncologyPatients { get; set; }
        DbSet<EvolutionCard> EvolutionCards { get; set; }
        DbSet<Appointment> Appointments { get; set; }
        DbSet<Person> People { get; set; }
        DbSet<HealthProfessional> HealthProfessionals { get; set; }
        DbSet<AppUser> AppUsers { get; set; }
        DbSet<Building> Buildings { get; set; }
        DbSet<HospitalUnit> HospitalUnits { get; set; }
        DbSet<Ward> Wards { get; set; }
        DbSet<Bed> Beds { get; set; }
        DbSet<AppointmentReason> AppointmentReasons { get; set; }
        DbSet<AmbulatoryAttentionRecord> AmbulatoryAttentionRecords { get; set; }

        DbSet<T> Set<T>() where T: class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

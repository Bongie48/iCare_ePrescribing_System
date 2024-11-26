using iCare_ePrescribing_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace iCare_ePrescribing_System.Data
{
    public class ApplicationDbContext : IdentityDbContext<StaffMembers>
    {
        //private readonly UserManager<IdentityUser> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");
            builder.Entity<StaffMembers>(
                entity => entity.ToTable(name: "AspNetUsers")
                );

           builder.Entity<MedContra>()
        .HasOne(mc => mc.ActiveIngredient)
        .WithMany()
        .HasForeignKey(mc => mc.CurrentActiveID)
        .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.NoAction

            builder.Entity<MedContra>()
                .HasOne(mc => mc.ActiveInteraction)
                .WithMany()
                .HasForeignKey(mc => mc.InteratingActiveID)
                .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.NoAction

            base.OnModelCreating(builder);

        }
        public DbSet<Vitals> Vitals { get; set; }
        public DbSet<Allergies> Allergies { get; set; }
        public DbSet<AddMedication> AddMedication { get; set; }
        public DbSet<DosageForm> DosageForm { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<ActiveIngredient> ActiveIngredients { get; set; }
        public DbSet<MedicationIngredient> MedicationIngredients { get; set; }
        public DbSet<MedicationOrder> MedicationOrders { get; set; }
        public DbSet<UserStock> UserStocks { get; set; }
        public DbSet<ReceivedStock> ReceivedStock { get; set; }
        public DbSet<Surgeon> Surgeon { get; set; }
        public DbSet<BookSurgery> Surgeries { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Theatre> Theatres { get; set; }
        public DbSet<TreatmentCode> TreatmentCodes { get; set; }
        public DbSet<PrescriptionItem> PrescriptionItem { get; set; }
        public DbSet<TestingDB> TestingDB { get; set; }
        public DbSet<AdminMedIngredients> AdminMedIngredients { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Anaesthesiologist> Anaesthesiologists { get; set; }
        public DbSet<Admission> Admission { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<Bed> Bed { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Suburb> Suburb { get; set; }
        public DbSet<PatientConditions> PatientConditions { get; set; }
        public DbSet<PatientMedication> PatientMedication { get; set; }
        public DbSet<PatientVitals> PatientVitals { get; set; }
        public DbSet<RegisteredStaff> Staff { get; set; }
        public DbSet<RejectPrescription> RejectPrescription { get;set; }
        public DbSet<DayHospital> DayHospitals { get; set; }
        public DbSet<Condition> Condition { get; set; }
        public DbSet<MedInteraction> MedInteraction { get; set; }   
        public DbSet<MedContra> MedContra { get; set; }
        public DbSet<PrescribedMedication> PrescribedMedication { get; set; }


    }
}
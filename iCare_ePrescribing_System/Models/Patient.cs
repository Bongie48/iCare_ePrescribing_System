using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace iCare_ePrescribing_System.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [Required]
        public string IDNo { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname {  get; set; }

        [Required]
        public string Gender { get; set; }

        public string? ContactNumber { get; set; }

        public string? Email { get; set; }

        public string? HomeAddress { get; set; }
        
        public Suburb? Suburb { get; set; }
        public int? SuburbId { get; set; }

        public ICollection<Admission>? Admission { get; set; }
        public ICollection<PatientMedication>? PatientMedication { get; set; }
        public ICollection<PatientConditions>? PatientConditions { get; set; }
        public ICollection<PatientVitals>? PatientVitals { get; set; }
        public ICollection<Allergies>? Allergies { get; set; }
        public ICollection<Medication> CurrentMedications { get; set; }
    }
}

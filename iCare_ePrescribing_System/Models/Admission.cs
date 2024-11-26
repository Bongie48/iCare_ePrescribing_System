using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iCare_ePrescribing_System.Models
{
    public class Admission
    {
        [Key]
        public int AdmisionID { get; set; }
        [Required]
        public DateTime AdmissionDate { get; set; }
        public DateTime? DischargeTime { get; set; }
        public string? AdmissionStatus { get; set; }
        [Required]
        public int Height { get; set; }
        [Required]
        public int Weight { get; set; }

        // nav prop
        public Patient Patient { get; set; }
        public int PatientID { get; set; }

        public Bed Bed { get; set; }
        public int BedID { get; set; }

        //[ForeignKey("NurseId")]
        //public virtual RegisteredStaff? Nurse { get; set; }

        public ICollection<PatientVitals> PatientVitals { get; set; }

    }
}

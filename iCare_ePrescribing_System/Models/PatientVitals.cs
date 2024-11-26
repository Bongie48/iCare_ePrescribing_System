using System.ComponentModel.DataAnnotations;
namespace iCare_ePrescribing_System.Models
{
    public class PatientVitals
    {
        [Key]
        public int PatientVitalsId { get; set; }
        [Required]
        public DateTime CaptureTime { get; set; }
        [Required]
        public string Readings { get; set; }

        public string? Status { get; set; }
        // nav prop
        public Vitals Vitals { get; set; }
        public int VitalId { get; set; }

        public Admission Admission { get; set; }
        public int AdmisionID { get; set; }
    }
}

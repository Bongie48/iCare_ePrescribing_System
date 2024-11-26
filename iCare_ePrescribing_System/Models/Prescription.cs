using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using iCare_ePrescribing_System.Data;

namespace iCare_ePrescribing_System.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionId { get; set; }
        [Required]
        public DateTime DateIssued { get; set; }
        [Required]
        public char UrgentStatus { get; set; }
        //public int MedicationId { get; set; }
        [Required]
        public string TrackStatus { get; set; }
        //Get surgeon
        public int MemberId { get; set; }
        [ForeignKey("MemberId")]
        public virtual RegisteredStaff RegisteredStaff { get; set; }
        //Get Patient
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }
        //public string MedicationName { get; set; }
        //public virtual DosageForm DosageForm {get;set;}

    }
}

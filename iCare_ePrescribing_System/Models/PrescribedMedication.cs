using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iCare_ePrescribing_System.Models
{
    public class PrescribedMedication
    {
        [Key]
        public int PrescribedMedicationId { get; set; }

        // Patient who received the medication
        public Patient Patient { get; set; }
        public int PatientId { get; set; }

        // Date and time of administration
        [Required]
        public DateTime TimeAdministered { get; set; }

        // Quantity of medication administered
        [Required]
        public int QuantityAdministered { get; set; }

        // Any additional instructions or notes from the nurse
        public string Notes { get; set; }

        [ForeignKey("PrescriptionItemId")]
        public PrescriptionItem? PrescriptionItem { get; set; }

        [ForeignKey("NurseId")]
        public virtual RegisteredStaff? Nurse { get; set; }
    }
}

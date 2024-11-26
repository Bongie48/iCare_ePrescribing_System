using System.ComponentModel.DataAnnotations;

namespace iCare_ePrescribing_System.Models
{
        public class PatientMedication
        {
            [Key]
            public int PatientMedicationId { get; set; }

            [Required]
            public int Quantity { get; set; }
            [Required]
            public string InTakeInstructions { get;set; }

            // nav prop
            public Medication Medication { get; set; }
            public int MedicationId { get; set; }

            public Patient Patient { get; set; }
            public int PatientId { get; set; }

        }
}

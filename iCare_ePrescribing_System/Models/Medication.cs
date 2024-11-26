using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace iCare_ePrescribing_System.Models
{
    public class Medication
    {
        [Key]
        public int MedicationId { get; set; }
        [Required]
        public string MedicationName { get; set; }
        
        //Dosage form
        public int dosageID { get; set; }
        [ForeignKey("dosageID")]
        public virtual DosageForm DosageForm { get; set; }

        //Schedule
        public int scheduleID { get; set; }
        [ForeignKey("scheduleID")]
        public virtual Schedule Schedule { get; set; }


        public ICollection<PatientMedication> PatientMedication { get; set; }
    }
}

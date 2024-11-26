using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace iCare_ePrescribing_System.Models
{
    public class AddMedication
    {
        //Medication to be used by pharmacist and surgeon
        [Key]
        public int MedicationId { get; set; }
        [Required]
        public string MedicationName { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int ReOrder { get; set; }
        [Required]
        public char ReOrderStatus { get; set; }

        //Dosage form
        public int dosageID { get; set; }
        [ForeignKey("dosageID")]
        public virtual DosageForm DosageForm { get; set; }

        //Schedule
        public int scheduleID { get; set; }
        [ForeignKey("scheduleID")]
        public virtual Schedule Schedule { get; set; }

        [Required]
        public int TrackStatus { get; set; }

        public virtual List<ReceivedStock> quantitylist { get; set; } = new List<ReceivedStock>();
        
        //public string PhotoUrl { get; set; }
        [Required(ErrorMessage = "Please choose a medication picture")]
        [NotMapped]
        public IFormFile ProfilePhoto { get; set; }
        public string? image { get;set; }

    }
}

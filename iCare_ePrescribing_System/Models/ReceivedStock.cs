using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace iCare_ePrescribing_System.Models
{
    public class ReceivedStock
    {
        [Key]
        public int ReceivedID { get;set; }
        [Required]
        public int ReceivedQuantity { get;set; }
        [Required]
        public int CurrentQuantity { get;set; }
        [Required]
        public DateTime ReceivedDate { get;set; }
        public string ReceivedStatus { get;set; }   
        //Medication
        public int MedicationID { get; set; }
        [ForeignKey("MedicationID")]
        public virtual AddMedication AddMedication { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iCare_ePrescribing_System.Models
{
    public class PrescriptionItem
    {
        [Key]
        public int itemId { get; set; }

        //Medication ID
        public int MedID { get; set; }
        [ForeignKey("MedID")]
        public virtual AddMedication medication { get; set; }

        //Prescription ID
        public int PresID { get; set; }
        [ForeignKey("PresID")]
        public virtual Prescription Prescription { get; set; }

        [Required] 
        public int Quantity { get;set; }
        [Required]
        public string instruction { get; set; }

        public int? RemaininingQuantity { get; set; }
    }
}

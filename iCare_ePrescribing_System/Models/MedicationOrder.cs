using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace iCare_ePrescribing_System.Models
{
    public class MedicationOrder
    {
        public MedicationOrder()
        {
        }
        [Key]
        public int OrderID { get; set; }
        [Required]
        public int OrderQuantity { get;set; }
        public string OrderStatus { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public int CurrentQuantity { get; set; }
        //Getting Medication
        public int medicineid { get; set; }
        [ForeignKey("medicineid")]
        public virtual AddMedication AddMedication { get; set; }

        //Email address to the purchaser
        public int StockID { get; set; }
        [ForeignKey("StockID")]
        public virtual DayHospital Stock { get; set; }
    }
}

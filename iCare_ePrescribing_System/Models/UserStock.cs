using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace iCare_ePrescribing_System.Models
{
    public class UserStock
    {

        [Key]
        public int StockID { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        //Getting all Order
        public virtual List<MedicationOrder> medicationOrders { get; set; } = new List<MedicationOrder>();
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace iCare_ePrescribing_System.Models
{
    public class DayHospital
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string PurchaseManagerEmail { get; set; }
        //Getting Order
        public virtual List<MedicationOrder> medicationOrders { get; set; } = new List<MedicationOrder>();
    }
}

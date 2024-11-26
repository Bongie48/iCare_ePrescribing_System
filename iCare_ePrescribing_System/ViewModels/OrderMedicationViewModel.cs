using iCare_ePrescribing_System.Data;
using iCare_ePrescribing_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace iCare_ePrescribing_System.ViewModels
{
    public class OrderMedicationViewModel
    {
        public MedicationOrder MedicationOrder { get; set; }
        public IEnumerable<MedicationOrder> orderlist { get; set; }
        public IEnumerable<SelectListItem> emailaddress { get; set; }
        public string Name { get;set; }
        public string dosage { get; set; }
        public int quantity { get; set; }
        public int reorder { get; set; }
        public int ID { get;set; }
    }
}
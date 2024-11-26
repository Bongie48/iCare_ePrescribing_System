using iCare_ePrescribing_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace iCare_ePrescribing_System.ViewModels
{
    public class GetOrderViewModel
    {
        public MedicationOrder order { get; set; }
        public UserStock stockuser { get; set; }
        public List<AddMedication> medicinelist { get; set; }
        public List<UserStock> users { get; set; }

        public List<AddMedication> QuickOrder { get; set; }
        public virtual List<MedicationOrder> QuickOrderQuantity { get; set; } = new List<MedicationOrder>();
        public string profile { get; set; }
    }
}

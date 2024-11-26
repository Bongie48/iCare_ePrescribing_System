using iCare_ePrescribing_System.Models;

namespace iCare_ePrescribing_System.ViewModels
{
    public class CaptureStockVM
    {
        public ReceivedStock receivedStock { get; set; }
        public List<AddMedication> MedicationList { get; set; }
        public AddMedication Medication { get; set; }
        public string profile { get; set; }
        public MedicationOrder medicationOrder { get; set; }
        public List<MedicationOrder> OrderList { get; set; }
    }
}

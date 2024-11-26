using iCare_ePrescribing_System.Models;

namespace iCare_ePrescribing_System.ViewModels
{
    public class OrdersVM
    {
        public IEnumerable<Prescription> Prescriptions { get; set; }
        public IEnumerable<PrescriptionItem> PrescriptionsItems { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TotalDispersed { get; set; }
        public int TotalRejected { get; set; }
        public int TotalMedcineDispersed { get; set; }
        // Medication totals for the graph
        public Dictionary<string, int> MedicationTotals { get; set; }

        public OrdersVM()
        {
            MedicationTotals = new Dictionary<string, int>();
        }

    }
}

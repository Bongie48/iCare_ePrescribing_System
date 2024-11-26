using iCare_ePrescribing_System.Models;

namespace iCare_ePrescribing_System.ViewModels.Nurse
{
    public class ReportsViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<PrescribedMedication>? PrescribedMedications { get; set; }
        public int TotalPatients { get; set; }
        public List<MedicationSummaryViewModel>? MedicationSummary { get; set; }
        public bool ReportGenerated { get; set; }
        public string NurseName { get; set; }
    }

    public class MedicationSummaryViewModel
    {
        public string? MedicationName { get; set; }
        public int QuantityAdministered { get; set; }
    }
}

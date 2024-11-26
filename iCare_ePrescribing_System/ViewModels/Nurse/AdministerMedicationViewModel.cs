using iCare_ePrescribing_System.Models;

namespace iCare_ePrescribing_System.ViewModels.Nurse
{
    public class AdministerMedicationViewModel
    {
        public Patient Patient { get; set; }
        public PrescriptionItem PrescriptionItem { get; set; }

        public int NurseId { get; set; }
        public int QuantityAdministered { get; set; }
        public DateTime AdministeredDate { get; set; }
        public string Instructions { get; set; } // Administering instructions
    }
}

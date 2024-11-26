using iCare_ePrescribing_System.Models;

namespace iCare_ePrescribing_System.ViewModels.Nurse
{
    public class ProfileViewModel
    {
        public Patient Patient { get; set; }
        public Admission Admission { get; set; }
        public ICollection<Allergies> Allergies { get; set; }
        public ICollection<PatientConditions> PatientConditions { get; set; }
        public ICollection<PatientMedication> PatientMedication { get; set; }
        public ICollection<PatientVitals> PatientVitals { get; set; }
        public ICollection<Condition> Condition { get; set; }

        // New addition: List of dispensed medications for the patient
        public ICollection<PrescriptionItem> PrescriptionItem { get; set; }
        public ICollection<PrescribedMedication> PrescribedMedication { get; set; }
    }
}
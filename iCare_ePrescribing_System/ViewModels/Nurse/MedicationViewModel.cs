using System;
using iCare_ePrescribing_System.Models;

namespace iCare_ePrescribing_System.ViewModels.Nurse;

public class MedicationViewModel
{
    public Patient Patient { get; set; }
    public ICollection<Medication> medications { get; set; }
    public List<PatientMedication> PatientMedicationList { get; set; } = new List<PatientMedication>();
}

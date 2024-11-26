using System;
using iCare_ePrescribing_System.Models;

namespace iCare_ePrescribing_System.ViewModels.Nurse
{
    public class VitalsViewModel
    {
        public Patient Patient { get; set; } // Patient info
        public Admission Admission { get; set; } // Admission related to the patient
        public List<Vitals> Vitals { get; set; } // List of vitals to choose from
        public List<PatientVitals> PatientVitals { get; set; } = new List<PatientVitals>(); // List of patient-specific vitals
    }

}

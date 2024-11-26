using iCare_ePrescribing_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace iCare_ePrescribing_System.ViewModels
{
    public class PrescriptionVM
    {
        public PrescriptionItem prescriptionItem { get; set; }
        public Prescription prescription { get; set; }
        public PatientVitals PatientVitalsId { get; set; }
        public IEnumerable<SelectListItem> Medication { get; set; }
        public IEnumerable<SelectListItem> Patients { get; set; }

        //Get surgeon id
        public IEnumerable<RegisteredStaff> SurgeonID { get; set; }
        //Get prescription id
        public IEnumerable<Prescription> prescriptionlist { get; set; }
        //Medication already prescribed
        public IEnumerable<PrescriptionItem> prescriptionitems { get; set; } 
        public string profile { get; set; }
        //public bool IsUrgent { get; set; }
        public IEnumerable<PatientVitals> Vitals { get; set; }

        public int patientId { get; set; }
    }
}

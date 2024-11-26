using iCare_ePrescribing_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace iCare_ePrescribing_System.ViewModels
{
    public class BookSurgeryVM
    {
        public int SurgeonId { get; set; }
        public List<SelectListItem> Patients { get; set; }
        public List<SelectListItem> Anaesthesiologists { get; set; }
        public List<SelectListItem> Theatres { get; set; }
        public List<SelectListItem> TreatmentCodes { get; set; }

        // For new patient creation
        public Patient NewPatient { get; set; }

        // Vitals information (data fetched from nurse)
        public List<Vitals> PatientVitals { get; set; }

        public DateTime SurgeryDate { get; set; }
        public string Session { get; set; }
        public int TheatreId { get; set; }
        public int TreatmentCode { get; set; }
    }
}

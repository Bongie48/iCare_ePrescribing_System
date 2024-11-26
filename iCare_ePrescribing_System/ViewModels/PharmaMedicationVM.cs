using iCare_ePrescribing_System.Data;
using iCare_ePrescribing_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace iCare_ePrescribing_System.ViewModels
{
    public class PharmaMedicationVM
    {
        //Adds medication
        public AddMedication AddMedication { get; set; }
        public IEnumerable<SelectListItem> dosage { get; set; }
        public IEnumerable<SelectListItem> schedule { get; set; }
        public string profile { get;set; }
    }
}

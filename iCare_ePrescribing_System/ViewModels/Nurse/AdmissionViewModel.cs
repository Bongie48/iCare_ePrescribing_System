using iCare_ePrescribing_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace iCare_ePrescribing_System.ViewModels.Nurse
{
    public class AdmissionViewModel
    {
        public Admission Admissions { get; set; }
        public Patient Patient { get; set; }    

        public int SelectedWardId { get; set; }
        public int SelectedProvinceId { get; set; }
        public int SelectedCityId { get; set; }
        public int SelectedSuburbId { get; set; }

        public IEnumerable<SelectListItem> WardList { get; set; }
        public IEnumerable<SelectListItem> BedList { get; set; }
        public IEnumerable<SelectListItem> ProvinceList { get; set; }
        public IEnumerable<SelectListItem> CityList { get; set; }
        public IEnumerable<SelectListItem> SuburbList { get; set; }
    }
}
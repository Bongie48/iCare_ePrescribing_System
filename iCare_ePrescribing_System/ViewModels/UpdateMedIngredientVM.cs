using iCare_ePrescribing_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace iCare_ePrescribing_System.ViewModels
{
    public class UpdateMedIngredientVM
    {
        public int medicationID { get; set; }
        public string Name { get; set; }   
        public int quantity { get; set; }
        public int reorder { get; set; }
        public int dosage { get; set; }
        public IEnumerable<SelectListItem> dosageform { get; set; }
        public IEnumerable<SelectListItem> Medschedule { get; set; }
        public IEnumerable<SelectListItem> ingredient { get; set; }
        public int schedule { get; set; }
        public char status { get; set; }
        public string image { get; set; }   
        public IEnumerable <int> activeid { get; set; }
        public IEnumerable<MedicationIngredient> active { get; set; }
        public string profile { get; set; }
        public int trackmedication { get; set; }
        public MedicationIngredient medicationIngredient { get; set; }
        public IEnumerable<SelectListItem> ingredientname { get; set; }
    }
}

using iCare_ePrescribing_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace iCare_ePrescribing_System.ViewModels
{
    public class IngredientViewModel
    {
        public MedicationIngredient medicationIngredient { get; set; }
        public IEnumerable<AddMedication> record { get; set; }
        public IEnumerable<SelectListItem> ingredientname { get; set; }
        public IEnumerable<MedicationIngredient> Currentingredient { get; set; }
        public string profile { get; set; }
    }
}

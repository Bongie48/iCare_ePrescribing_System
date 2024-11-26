using iCare_ePrescribing_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime;

namespace iCare_ePrescribing_System.ViewModels
{
    public class GetMedicineViewModel
    {
        //Get Medicine and Active ingredient
        public IEnumerable<AddMedication> medicationlist {  get; set; } 
        public IEnumerable<MedicationIngredient> ingredientlist { get; set; }
        public string profile { get; set; }
    }
}

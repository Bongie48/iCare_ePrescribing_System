using iCare_ePrescribing_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace iCare_ePrescribing_System.ViewModels
{
    public class MedicationInteractionVM
    {
        public MedContra MedContra { get; set; }
        [DisplayName("Active Ingredients")]
        public IList<SelectListItem>? Active { get; set; }
        public IEnumerable<SelectListItem> ActiveName { get; set; }
        //Get DB
        public IEnumerable<MedContra> MedInteractionsList { get; set; }

        // This is the list of selected active ingredient IDs
        public List<int> SelectedActiveIngredientIDs { get; set; } = new List<int>();
    }
}

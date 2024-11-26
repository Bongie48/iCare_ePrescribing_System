using iCare_ePrescribing_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace iCare_ePrescribing_System.ViewModels
{
    public class ContraIndicationVM
    {
        public MedInteraction MedInteraction { get; set; }
        [DisplayName("Active Ingredients")]
        public IList<SelectListItem>? Active { get; set; }
        public IEnumerable<SelectListItem> ConditionName { get; set; }
        public IEnumerable<MedInteraction> MedInteractionsList { get; set; }

        // This is the list of selected active ingredient IDs
        public List<int> SelectedActiveIngredientIDs { get; set; } = new List<int>();

    }
}

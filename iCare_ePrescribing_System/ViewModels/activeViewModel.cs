using iCare_ePrescribing_System.Models;

namespace iCare_ePrescribing_System.ViewModels
{
    public class activeViewModel
    {
        public ActiveIngredient ActiveIngredient { get; set; }
        public IEnumerable<ActiveIngredient> ingredientslist { get; set;}
    }
}

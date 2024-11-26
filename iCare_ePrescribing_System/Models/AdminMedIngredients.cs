using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace iCare_ePrescribing_System.Models
{
    public class AdminMedIngredients
    {
        [Key]
        public int AdminIngredientId { get; set; }

        public int Medactiveid { get; set; }
        [ForeignKey("Medactiveid")]
        public virtual ActiveIngredient ActiveIngredient { get; set; }

        public int adminmedicineid { get; set; }
        [ForeignKey("adminmedicineid")]
        public virtual Medication Medication { get; set; }

        [Required]
        public double Strength { get; set; }

        //Getting medications and active ingredients
        public virtual List<AdminMedIngredients> currentMedicationActive { get; set; } = new List<AdminMedIngredients>();
    }
}
